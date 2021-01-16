using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohkazv.Library.WebApp.Data.Migrations
{
    public partial class AlterDecimalClassifications01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_DeweyDecimalClassifications_DdcId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeweyDecimalClassifications",
                table: "DeweyDecimalClassifications");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DeweyDecimalClassifications");

            migrationBuilder.AddColumn<int>(
                name: "Temp",
                table: "DeweyDecimalClassifications",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "DdcId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeweyDecimalClassifications",
                table: "DeweyDecimalClassifications",
                column: "Temp");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_DeweyDecimalClassifications_DdcId",
                table: "Books",
                column: "DdcId",
                principalTable: "DeweyDecimalClassifications",
                principalColumn: "Temp",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_DeweyDecimalClassifications_DdcId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeweyDecimalClassifications",
                table: "DeweyDecimalClassifications");

            migrationBuilder.DropColumn(
                name: "Temp",
                table: "DeweyDecimalClassifications");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "DeweyDecimalClassifications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "DdcId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeweyDecimalClassifications",
                table: "DeweyDecimalClassifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_DeweyDecimalClassifications_DdcId",
                table: "Books",
                column: "DdcId",
                principalTable: "DeweyDecimalClassifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
