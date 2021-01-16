using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;
using System.Text;

namespace Mohkazv.Library.WebApp.Data.Migrations
{
    public partial class InsertDefaultLanguages01 : Migration
    {
        private readonly string[] languages = new string[] { "آذرباﻳﺠﺎﻧﻰ",
"آلبانیایی", "آلمانی", "اردو", "ارمنی", "ازبکی", "اسپانیایی", "اسپرانتو", "استونيايی", "اسلواکی", "اسلونیایی", "افریکانس", "اکراينی", "امهری", "اندونزيايی", "انگلیسی", "اودیه (اوریه)", "اویغوری", "ایتالیایی", "ایرلندی", "ايسلندی", "ایگبو", "باسکی", "برمه‌ای",
"بلاروسی", "بلغاری", "بنگالی", "بوسنیایی", "پرتغالی", "پشتو", "پنجابی", "تاتار", "تاجیک", "تاميلی", "تايلندی", "ترکمنی", "ترکی استانبولی", "تلوگو", "جاوه‌ای", "چک", "چوایی", "چینی (ساده‌شده)", "چینی (سنتی)", "خمری", "خوسایی", "دانمارکی", "روسی", "رومانيايی", "زولو", "ژاپنی", "ساموایی", "سبوانو", "سندی", "سواهيلی", "سوئدی", "سوتو", "سودانی", "سومالیایی", "سینهالی", "شونا", "صربی", "عبری", "عربی", "فارسی", "فرانسوی", "فريسی", "فنلاندی", "فیلیپینی", "قرقیزی", "قزاقی", "کاتالان", "کانارا", "کرئول هائیتی", "کردی", "كرسی", "کرواتی", "کره‌ای", "کینیارواندا", "گالیسی", "گاليک اسکاتلندی", "گجراتی", "گرجی", "لائوسی", "لاتين", "لتونيايی", "لوگزامبورگی", "لهستانی", "ليتوانيايی", "مائوری", "مالاگاسی", "مالایالمی", "مالايی", "مالتی", "مجاری", "مراتی", "مغولی", "مقدونيه‌ای", "نپالی", "نروژی", "ولزی", "ويتنامی", "هاوایی", "هلندی", "همونگ", "هندی", "هوسا", "یدیشی", "یوروبایی", "يونانی" };

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            InsertDefaultLanguages.DownMethod(migrationBuilder);

            var ddcs = new StringBuilder();
            foreach (var lang in languages)
                ddcs.Append($"INSERT INTO [Languages] ([Name]) VALUES (N'{lang}')\n");
            migrationBuilder.Sql(ddcs.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            InsertDefaultLanguages.UpMethod(migrationBuilder);

            var ddcs = new StringBuilder();
            foreach (var lang in languages)
                ddcs.Append($"DELETE FROM [Languages] WHERE ([Name] = N'{lang}')\n");
            migrationBuilder.Sql(ddcs.ToString());
        }
    }
}
