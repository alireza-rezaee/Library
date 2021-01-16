using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;

namespace Mohkazv.Library.WebApp.Data.Migrations
{
    public partial class InsertDefaultLanguages : Migration
    {
        public static void UpMethod(MigrationBuilder migrationBuilder)
            => migrationBuilder.Sql(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"Data\SqlQueries\20210111175829_InsertDefaultLanguages\InsertDefaultLanguages_Up.sql")));

        public static void DownMethod(MigrationBuilder migrationBuilder)
            => migrationBuilder.Sql(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"Data\SqlQueries\20210111175829_InsertDefaultLanguages\InsertDefaultLanguages_Down.sql")));

        protected override void Up(MigrationBuilder migrationBuilder) => UpMethod(migrationBuilder);

        protected override void Down(MigrationBuilder migrationBuilder) => DownMethod(migrationBuilder);
    }
}
