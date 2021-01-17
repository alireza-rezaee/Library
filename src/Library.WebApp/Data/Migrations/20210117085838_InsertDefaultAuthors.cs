using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;

namespace Mohkazv.Library.WebApp.Data.Migrations
{
    public partial class InsertDefaultAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
            => migrationBuilder.Sql(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"Data\SqlQueries\20210117085838_InsertDefaultAuthors\InsertDefaultAuthors_Up.sql")));

        protected override void Down(MigrationBuilder migrationBuilder)
            => migrationBuilder.Sql(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"Data\SqlQueries\20210117085838_InsertDefaultAuthors\InsertDefaultAuthors_Down.sql")));
    }
}
