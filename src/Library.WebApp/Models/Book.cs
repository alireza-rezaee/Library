﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mohkazv.Library.WebApp.Models
{
    public class Book
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 13, ErrorMessage = "{0} کتاب میان {2} تا {1} کاراکتر طول دارد.")]
        [Display(Name = "شابک")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} کتاب الزامی است.")]
        [MaxLength(250, ErrorMessage = "{0} کتاب تا سقف {1} کاراکتر طول دارد.")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "تصویر")]
        [MaxLength(300, ErrorMessage = "{0} کتاب تا سقف {1} کاراکتر طول دارد.")]
        public string CoverPath { get; set; }

        [Display(Name = "زبان")]
        [ForeignKey(nameof(Language))]
        public int LanguageId { get; set; }

        [Display(Name = "ناشر")]
        [ForeignKey(nameof(Publisher))]
        public int? PublisherId { get; set; }

        [Display(Name = "رده‌بندی دیوئی")]
        [ForeignKey(nameof(DeweyDecimalClassification))]
        public int? DdcId { get; set; }

        #region Relationships
        [Display(Name = "زبان")]
        public Language Language { get; set; }

        [Display(Name = "نویسنده(ها)")]
        public ICollection<BookAuthor> BookAuthors { get; set; }

        [Display(Name = "ناشر")]
        public Publisher Publisher { get; set; }

        [Display(Name = "رده‌بندی دهدهی دیوئی")]
        public DeweyDecimalClassification DeweyDecimalClassification { get; set; }

        public ICollection<BorrowBook> BorrowBooks { get; set; }
        #endregion
    }
}
