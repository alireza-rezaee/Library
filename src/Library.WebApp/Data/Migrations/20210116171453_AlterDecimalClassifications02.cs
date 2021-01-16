using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohkazv.Library.WebApp.Data.Migrations
{
    public partial class AlterDecimalClassifications02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Temp",
                table: "DeweyDecimalClassifications",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DeweyDecimalClassifications",
                newName: "Temp");
        }
    }
}
