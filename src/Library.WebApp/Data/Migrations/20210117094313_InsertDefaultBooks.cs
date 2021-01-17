using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;

namespace Mohkazv.Library.WebApp.Data.Migrations
{
    public partial class InsertDefaultBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
            => migrationBuilder.Sql(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"Data\SqlQueries\20210117094313_InsertDefaultBooks\InsertDefaultBooks_Up.sql")));

        protected override void Down(MigrationBuilder migrationBuilder)
            => migrationBuilder.Sql(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"Data\SqlQueries\20210117094313_InsertDefaultBooks\InsertDefaultBooks_Down.sql")));
    }
}
