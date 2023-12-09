using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
//Add-Migration -Context "WarhammerNarrative_Context" -Name "Migration_v0.10" -outputdir "EntityFramework/WarhammerNarrative/Migrations"
namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Migrations
{
    /// <inheritdoc />
    public partial class Migration_v010 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Critical",
                table: "DiceRoll",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IgnoreNextRoll",
                table: "DiceRoll",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Critical",
                table: "DiceRoll");

            migrationBuilder.DropColumn(
                name: "IgnoreNextRoll",
                table: "DiceRoll");
        }
    }
}
