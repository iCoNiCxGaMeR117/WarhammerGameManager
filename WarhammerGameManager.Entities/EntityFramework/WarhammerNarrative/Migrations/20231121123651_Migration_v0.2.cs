using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
//Add-Migration -Context "WarhammerNarrative_Context" -Name "Migration_v0.2" -outputdir "EntityFramework/WarhammerNarrative/Migrations"
namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Migrations
{
    /// <inheritdoc />
    public partial class Migration_v02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "GameDate",
                table: "GameResult",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameDate",
                table: "GameResult");
        }
    }
}
