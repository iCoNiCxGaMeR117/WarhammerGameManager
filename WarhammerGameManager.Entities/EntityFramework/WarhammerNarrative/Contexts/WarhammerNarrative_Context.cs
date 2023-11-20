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
    public virtual DbSet<Player> Players { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:WHNCon");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DiceEvent>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(DiceEvent)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasMany(d => d.HitRolls)
            .WithOne(p => p.Event);

            entity.HasMany(d => d.WoundRolls)
            .WithOne(p => p.Event);

            entity.HasMany(d => d.SaveRolls)
            .WithOne(p => p.Event);

            entity.HasMany(d => d.FeelNoPainRolls)
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

            entity.HasMany(d => d.PlayerFactions)
            .WithMany(p => p.Factions);

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

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable($"{nameof(Player)}");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasMany(d => d.Factions)
            .WithMany(p => p.PlayerFactions);

            entity.HasMany(d => d.Games)
            .WithOne(p => p.PlayerData);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
