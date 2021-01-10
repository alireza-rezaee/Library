using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohkazv.Library.WebApp.Data.Migrations
{
    public partial class AlterBorrowBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDateTime",
                table: "BorrowBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDateTime",
                table: "BorrowBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnedDateTime",
                table: "BorrowBooks",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDateTime",
                table: "BorrowBooks");

            migrationBuilder.DropColumn(
                name: "ReturnDateTime",
                table: "BorrowBooks");

            migrationBuilder.DropColumn(
                name: "ReturnedDateTime",
                table: "BorrowBooks");
        }
    }
}
