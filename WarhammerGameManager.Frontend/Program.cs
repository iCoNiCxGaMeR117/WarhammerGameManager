using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Contexts;
using WarhammerGameManager.Frontend.Hubs;
using WarhammerGameManager.Logic.Logical.Classes;
using WarhammerGameManager.Logic.Logical.Interfaces;

//https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio
//TODO: Integrate auth into application for account management and security.
//TODO: Get an SSL cert for secure transmission
//Testing Branch Policies
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    //builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    //   .AddNegotiate();

    //builder.Services.AddAuthorization(options =>
    //{
    //    // By default, all incoming requests will be authorized according to the default policy.
    //    options.FallbackPolicy = options.DefaultPolicy;
    //});
    builder.Services.AddRazorPages();
    builder.Services.AddSignalR();

    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.Debug()
        .WriteTo.MSSqlServer(
        connectionString: builder.Configuration["ConnectionStrings:WHNCon"],
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "LogEvents",
            SchemaName = "dbo",
            AutoCreateSqlTable = true
        },
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
        formatProvider: null,
        columnOptions: null,
        logEventFormatter: null
    ).CreateBootstrapLogger();

    builder.Host.UseSerilog();

    //Add class injections
    builder.Services.AddDbContext<WarhammerNarrative_Context>(o => o.UseSqlServer("Name=ConnectionStrings:WHNCon", s => s.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
    builder.Services.AddScoped<IAdminLogic, AdminLogic>();
    builder.Services.AddScoped<IGameManagerLogic, GameManagerLogic>();
    builder.Services.AddSingleton<Random>();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapHub<GameUpdateHub>("/gameUpdateHub");

    Log.Information("Application starting!");

    app.Run();
}
catch (Exception ex)
{
    Log.Error(ex, "Could not start application! {ExceptionName} - {ExceptionMessage}", ex.GetType(), ex.Message);
}
finally
{
    Log.Information("Closing Application and flushing logs.");
    Log.CloseAndFlush();
}