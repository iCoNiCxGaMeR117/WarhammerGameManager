using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
// Add-Migration -Context "WarhammerNarrative_Context" -Name "Migration_v0.1" -outputdir "EntityFramework/WarhammerNarrative/Migrations"
namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Migrations
{
    /// <inheritdoc />
    public partial class Migration_v01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RollTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RollTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiceEvent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameRollId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiceEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiceEvent_GameResult_GameRollId",
                        column: x => x.GameRollId,
                        principalTable: "GameResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "GameData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerDataId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerFactionId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameData_Faction_PlayerFactionId",
                        column: x => x.PlayerFactionId,
                        principalTable: "Faction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameData_GameResult_GameId",
                        column: x => x.GameId,
                        principalTable: "GameResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameData_Player_PlayerDataId",
                        column: x => x.PlayerDataId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiceRoll",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RollResult = table.Column<int>(type: "int", nullable: false),
                    PassResult = table.Column<bool>(type: "bit", nullable: false),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    RollTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiceRoll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiceRoll_DiceEvent_EventId",
                        column: x => x.EventId,
                        principalTable: "DiceEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiceRoll_RollTypes_RollTypeId",
                        column: x => x.RollTypeId,
                        principalTable: "RollTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiceEvent_GameRollId",
                table: "DiceEvent",
                column: "GameRollId");

            migrationBuilder.CreateIndex(
                name: "IX_DiceRoll_EventId",
                table: "DiceRoll",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_DiceRoll_RollTypeId",
                table: "DiceRoll",
                column: "RollTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FactionPlayer_PlayerFactionsId",
                table: "FactionPlayer",
                column: "PlayerFactionsId");

            migrationBuilder.CreateIndex(
                name: "IX_GameData_GameId",
                table: "GameData",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameData_PlayerDataId",
                table: "GameData",
                column: "PlayerDataId");

            migrationBuilder.CreateIndex(
                name: "IX_GameData_PlayerFactionId",
                table: "GameData",
                column: "PlayerFactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiceRoll");

            migrationBuilder.DropTable(
                name: "FactionPlayer");

            migrationBuilder.DropTable(
                name: "GameData");

            migrationBuilder.DropTable(
                name: "DiceEvent");

            migrationBuilder.DropTable(
                name: "RollTypes");

            migrationBuilder.DropTable(
                name: "Faction");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "GameResult");
        }
    }
}
