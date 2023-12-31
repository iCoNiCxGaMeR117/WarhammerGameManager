﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Contexts;

#nullable disable

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Migrations
{
    [DbContext(typeof(WarhammerNarrative_Context))]
    [Migration("20231124115955_Migration_v0.4")]
    partial class Migration_v04
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PlayerSubFaction", b =>
                {
                    b.Property<long>("PlayerFactionsId")
                        .HasColumnType("bigint");

                    b.Property<long>("SubFactionsId")
                        .HasColumnType("bigint");

                    b.HasKey("PlayerFactionsId", "SubFactionsId");

                    b.HasIndex("SubFactionsId");

                    b.ToTable("PlayerSubFaction");
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.DiceEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("GameRollId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GameRollId");

                    b.ToTable("DiceEvent", (string)null);
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.DiceRoll", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<bool>("PassResult")
                        .HasColumnType("bit");

                    b.Property<int>("RollResult")
                        .HasColumnType("int");

                    b.Property<long>("RollTypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("RollTypeId");

                    b.ToTable("DiceRoll", (string)null);
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.Faction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ParentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Faction", (string)null);
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.GameData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<long>("PlayerDataId")
                        .HasColumnType("bigint");

                    b.Property<long>("PlayerFactionId")
                        .HasColumnType("bigint");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerDataId");

                    b.HasIndex("PlayerFactionId");

                    b.ToTable("GameData", (string)null);
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.GameResult", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("GameDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("GameResult", (string)null);
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.ParentFaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ParentFaction", (string)null);
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Player", (string)null);
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.RollType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RollType", (string)null);
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.SubFaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("FactionId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FactionId");

                    b.ToTable("SubFaction", (string)null);
                });

            modelBuilder.Entity("PlayerSubFaction", b =>
                {
                    b.HasOne("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayerFactionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.SubFaction", null)
                        .WithMany()
                        .HasForeignKey("SubFactionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.DiceEvent", b =>
                {
                    b.HasOne("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.GameResult", "GameRoll")
                        .WithMany("DiceEvents")
                        .HasForeignKey("GameRollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameRoll");
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.DiceRoll", b =>
                {
                    b.HasOne("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.DiceEvent", "Event")
                        .WithMany("Rolls")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.RollType", "RollType")
                        .WithMany("DiceRolls")
                        .HasForeignKey("RollTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("RollType");
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.Faction", b =>
                {
                    b.HasOne("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.ParentFaction", "Parent")
                        .WithMany("ChildFactions")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.GameData", b =>
                {
                    b.HasOne("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.GameResult", "Game")
                        .WithMany("GamePlayData")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.Player", "PlayerData")
                        .WithMany("Games")
                        .HasForeignKey("PlayerDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.Faction", "PlayerFaction")
                        .WithMany("Games")
                        .HasForeignKey("PlayerFactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("PlayerData");

                    b.Navigation("PlayerFaction");
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.SubFaction", b =>
                {
                    b.HasOne("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.Faction", "Faction")
                        .WithMany("SubFactions")
                        .HasForeignKey("FactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faction");
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.DiceEvent", b =>
                {
                    b.Navigation("Rolls");
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.Faction", b =>
                {
                    b.Navigation("Games");

                    b.Navigation("SubFactions");
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.GameResult", b =>
                {
                    b.Navigation("DiceEvents");

                    b.Navigation("GamePlayData");
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.ParentFaction", b =>
                {
                    b.Navigation("ChildFactions");
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.Player", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels.RollType", b =>
                {
                    b.Navigation("DiceRolls");
                });
#pragma warning restore 612, 618
        }
    }
}
