using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Contexts;

public partial class WarhammerNarrative_Context : DbContext
{
    //Scaffold-DbContext "Name=ConnectionStrings:WHNCon" Microsoft.EntityFrameworkCore.SqlServer -Context "WarhammerNarrative_Context" -ContextDir "EntityFramework/WarhammerNarrative/Contexts" -OutputDir "EntityFramework/WarhammerNarrative/TableModels" -Force
    public WarhammerNarrative_Context()
    {
    }

    public WarhammerNarrative_Context(DbContextOptions<WarhammerNarrative_Context> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:WHNCon");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
