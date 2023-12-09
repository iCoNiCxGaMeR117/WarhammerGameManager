using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using WarhammerGameManager.Entities.EntityFramework.IdentityHub.Contexts;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Contexts;
using WarhammerGameManager.Frontend.Hubs;
using WarhammerGameManager.Logic.Logical.Classes;
using WarhammerGameManager.Logic.Logical.Interfaces;

//TODO: Get an SSL cert for secure transmission
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityHub_Context>();

    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
    });

    builder.Services.ConfigureApplicationCookie(options =>
    {
        // Cookie settings
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

        options.LoginPath = "/Identity/Account/Login";
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        options.SlidingExpiration = true;
    });

    builder.Services.AddAuthorization(options =>
    {
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
    });

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
    builder.Services.AddDbContext<IdentityHub_Context>(options => options.UseSqlServer("Name=ConnectionStrings:AuthCon"));
    builder.Services.AddScoped<IAdminLogic, AdminLogic>();
    builder.Services.AddScoped<IGameManagerLogic, GameManagerLogic>();
    builder.Services.AddScoped<IRollDiceLogic, RollDiceLogic>();
    builder.Services.AddSingleton<Random>();
    builder.Services.AddTransient<IEmailSender, EmailSender>();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapRazorPages();

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