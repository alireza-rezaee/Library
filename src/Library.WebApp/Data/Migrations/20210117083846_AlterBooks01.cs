using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohkazv.Library.WebApp.Data.Migrations
{
    public partial class AlterBooks01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_DeweyDecimalClassifications_DdcId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Types_TypeId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Books_TypeId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PublishInformation",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "DdcId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_DeweyDecimalClassifications_DdcId",
                table: "Books",
                column: "DdcId",
                principalTable: "DeweyDecimalClassifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_DeweyDecimalClassifications_DdcId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "DdcId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublishInformation",
                table: "Books",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_TypeId",
                table: "Books",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_DeweyDecimalClassifications_DdcId",
                table: "Books",
                column: "DdcId",
                principalTable: "DeweyDecimalClassifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Types_TypeId",
                table: "Books",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
