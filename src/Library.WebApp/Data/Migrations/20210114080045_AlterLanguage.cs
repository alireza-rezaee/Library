using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohkazv.Library.WebApp.Data.Migrations
{
    public partial class AlterLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Languages_LanguageName",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Books_LanguageName",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LanguageName",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Languages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LanguageId",
                table: "Books",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Languages_LanguageId",
                table: "Books",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Languages_LanguageId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Books_LanguageId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "LanguageName",
                table: "Books",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LanguageName",
                table: "Books",
                column: "LanguageName");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Languages_LanguageName",
                table: "Books",
                column: "LanguageName",
                principalTable: "Languages",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
