using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
//Add-Migration -Context "WarhammerNarrative_Context" -Name "Migration_v0.11" -outputdir "EntityFramework/WarhammerNarrative/Migrations"
namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Migrations
{
    /// <inheritdoc />
    public partial class Migration_v011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApplyLast",
                table: "GameRule",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyLast",
                table: "GameRule");
        }
    }
}
