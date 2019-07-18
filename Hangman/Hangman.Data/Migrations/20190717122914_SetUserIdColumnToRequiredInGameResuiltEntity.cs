using Microsoft.EntityFrameworkCore.Migrations;

namespace Hangman.Data.Migrations
{
    public partial class SetUserIdColumnToRequiredInGameResuiltEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameResults_Users_UserId",
                table: "GameResults");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GameResults",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameResults_Users_UserId",
                table: "GameResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameResults_Users_UserId",
                table: "GameResults");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GameResults",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_GameResults_Users_UserId",
                table: "GameResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
