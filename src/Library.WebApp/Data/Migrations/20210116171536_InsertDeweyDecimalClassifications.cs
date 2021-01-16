using Microsoft.EntityFrameworkCore.Migrations;
using Mohkazv.Library.WebApp.Extensions;
using System.Text;

namespace Mohkazv.Library.WebApp.Data.Migrations
{
    public partial class InsertDeweyDecimalClassifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var ddcs = new StringBuilder(3 * 1000);
            for (var i = 0; i < 1000; i++)
                ddcs.Append($"INSERT INTO [DeweyDecimalClassifications] ([Title]) VALUES (N'{$"{i:000}".EnglishNumberToPersian()}')\n");
            migrationBuilder.Sql(ddcs.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var ddcs = new StringBuilder(3 * 1000);
            for (var i = 0; i < 1000; i++)
                ddcs.Append($"DELETE FROM [DeweyDecimalClassifications] WHERE ([Title] = N'{$"{i:000}".EnglishNumberToPersian()}')\n");
            migrationBuilder.Sql(ddcs.ToString());
        }
    }
}