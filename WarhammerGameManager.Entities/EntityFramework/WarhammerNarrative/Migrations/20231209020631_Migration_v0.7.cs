using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
//Add-Migration -Context "WarhammerNarrative_Context" -Name "Migration_v0.7" -outputdir "EntityFramework/WarhammerNarrative/Migrations"
namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Migrations
{
    /// <inheritdoc />
    public partial class Migration_v07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RuleAppliesTo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleAppliesTo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppliesToId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRule_RuleAppliesTo_AppliesToId",
                        column: x => x.AppliesToId,
                        principalTable: "RuleAppliesTo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameRule_AppliesToId",
                table: "GameRule",
                column: "AppliesToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameRule");

            migrationBuilder.DropTable(
                name: "RuleAppliesTo");
        }
    }
}
