using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
//Add-Migration -Context "WarhammerNarrative_Context" -Name "Migration_v0.3" -outputdir "EntityFramework/WarhammerNarrative/Migrations"
namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Migrations
{
    /// <inheritdoc />
    public partial class Migration_v03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactionPlayer");

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Faction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ParentFaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentFaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubFaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubFaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubFaction_Faction_FactionId",
                        column: x => x.FactionId,
                        principalTable: "Faction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSubFaction",
                columns: table => new
                {
                    PlayerFactionsId = table.Column<long>(type: "bigint", nullable: false),
                    SubFactionsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSubFaction", x => new { x.PlayerFactionsId, x.SubFactionsId });
                    table.ForeignKey(
                        name: "FK_PlayerSubFaction_Player_PlayerFactionsId",
                        column: x => x.PlayerFactionsId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerSubFaction_SubFaction_SubFactionsId",
                        column: x => x.SubFactionsId,
                        principalTable: "SubFaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faction_ParentId",
                table: "Faction",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSubFaction_SubFactionsId",
                table: "PlayerSubFaction",
                column: "SubFactionsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubFaction_FactionId",
                table: "SubFaction",
                column: "FactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faction_ParentFaction_ParentId",
                table: "Faction",
                column: "ParentId",
                principalTable: "ParentFaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faction_ParentFaction_ParentId",
                table: "Faction");

            migrationBuilder.DropTable(
                name: "ParentFaction");

            migrationBuilder.DropTable(
                name: "PlayerSubFaction");

            migrationBuilder.DropTable(
                name: "SubFaction");

            migrationBuilder.DropIndex(
                name: "IX_Faction_ParentId",
                table: "Faction");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Faction");

            migrationBuilder.CreateTable(
                name: "FactionPlayer",
                columns: table => new
                {
                    FactionsId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerFactionsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactionPlayer", x => new { x.FactionsId, x.PlayerFactionsId });
                    table.ForeignKey(
                        name: "FK_FactionPlayer_Faction_FactionsId",
                        column: x => x.FactionsId,
                        principalTable: "Faction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactionPlayer_Player_PlayerFactionsId",
                        column: x => x.PlayerFactionsId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FactionPlayer_PlayerFactionsId",
                table: "FactionPlayer",
                column: "PlayerFactionsId");
        }
    }
}
