using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
//Add-Migration -Context "WarhammerNarrative_Context" -Name "Migration_v0.4" -outputdir "EntityFramework/WarhammerNarrative/Migrations"
namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Migrations
{
    /// <inheritdoc />
    public partial class Migration_v04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiceRoll_RollTypes_RollTypeId",
                table: "DiceRoll");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RollTypes",
                table: "RollTypes");

            migrationBuilder.RenameTable(
                name: "RollTypes",
                newName: "RollType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RollType",
                table: "RollType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiceRoll_RollType_RollTypeId",
                table: "DiceRoll",
                column: "RollTypeId",
                principalTable: "RollType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiceRoll_RollType_RollTypeId",
                table: "DiceRoll");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RollType",
                table: "RollType");

            migrationBuilder.RenameTable(
                name: "RollType",
                newName: "RollTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RollTypes",
                table: "RollTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiceRoll_RollTypes_RollTypeId",
                table: "DiceRoll",
                column: "RollTypeId",
                principalTable: "RollTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
