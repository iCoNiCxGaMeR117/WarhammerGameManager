using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
//Add-Migration -Context "WarhammerNarrative_Context" -Name "Migration_v0.5" -outputdir "EntityFramework/WarhammerNarrative/Migrations"
namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Migrations
{
    /// <inheritdoc />
    public partial class Migration_v05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FirstResultId",
                table: "DiceRoll",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Reroll",
                table: "DiceRoll",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_DiceRoll_FirstResultId",
                table: "DiceRoll",
                column: "FirstResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiceRoll_DiceRoll_FirstResultId",
                table: "DiceRoll",
                column: "FirstResultId",
                principalTable: "DiceRoll",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiceRoll_DiceRoll_FirstResultId",
                table: "DiceRoll");

            migrationBuilder.DropIndex(
                name: "IX_DiceRoll_FirstResultId",
                table: "DiceRoll");

            migrationBuilder.DropColumn(
                name: "FirstResultId",
                table: "DiceRoll");

            migrationBuilder.DropColumn(
                name: "Reroll",
                table: "DiceRoll");
        }
    }
}
