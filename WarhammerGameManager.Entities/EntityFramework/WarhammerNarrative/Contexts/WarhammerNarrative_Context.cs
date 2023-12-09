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

    public virtual DbSet<DiceEvent> DiceEvents { get; set; }
    public virtual DbSet<DiceRoll> DiceRolls { get; set; }
    public virtual DbSet<Faction> Factions { get; set; }
    public virtual DbSet<GameData> GameDatas { get; set; }
    public virtual DbSet<GameResult> GameResults { get; set; }
    public virtual DbSet<GameRule> GameRules { get; set; }
    public virtual DbSet<ParentFaction> ParentFactions { get; set; }
    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<RollType> RollTypes { get; set; }
    public virtual DbSet<RuleAppliesTo> RuleAppliesTos { get; set; }
    public virtual DbSet<SubFaction> SubFactions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Name=ConnectionStrings:WHNCon", s => s.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DiceEvent>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(DiceEvent)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasMany(d => d.Rolls)
            .WithOne(p => p.Event);
        });

        modelBuilder.Entity<DiceRoll>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(DiceRoll)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        });

        modelBuilder.Entity<Faction>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(Faction)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasMany(d => d.SubFactions)
            .WithOne(p => p.Faction);

            entity.HasMany(d => d.Games)
            .WithOne(p => p.PlayerFaction);
        });

        modelBuilder.Entity<GameData>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(GameData)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        });

        modelBuilder.Entity<GameResult>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(GameResult)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasMany(d => d.GamePlayData)
            .WithOne(p => p.Game);

            entity.HasMany(d => d.DiceEvents)
            .WithOne(p => p.GameRoll);
        });

        modelBuilder.Entity<GameRule>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(GameRule)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        });

        modelBuilder.Entity<ParentFaction>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(ParentFaction)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasMany(d => d.ChildFactions)
            .WithOne(p => p.Parent);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(Player)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasMany(d => d.SubFactions)
            .WithMany(p => p.PlayerFactions);

            entity.HasMany(d => d.Games)
            .WithOne(p => p.PlayerData);
        });

        modelBuilder.Entity<RollType>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(RollType)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasMany(d => d.DiceRolls)
            .WithOne(p => p.RollType);
        });

        modelBuilder.Entity<RuleAppliesTo>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(RuleAppliesTo)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasMany(d => d.GameRules)
            .WithOne(p => p.AppliesTo);
        });

        modelBuilder.Entity<SubFaction>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(SubFaction)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasMany(d => d.PlayerFactions)
            .WithMany(p => p.SubFactions);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
