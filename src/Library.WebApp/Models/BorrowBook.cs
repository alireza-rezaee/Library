using Mohkazv.Library.WebApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Models
{
    public class BorrowBook
    {
        public int BookId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "زمان تحویل از کتابخانه")]
        public DateTime? DeliveryDateTime { get; set; }

        [Display(Name = "موعد پس‌دادن به کتابخانه")]
        public DateTime? ReturnDateTime { get; set; }

        [Display(Name = "زمان پس‌دادن به کتابخانه")]
        public DateTime? ReturnedDateTime { get; set; }

        #region Relationships
        public Book Book { get; set; }

        public ApplicationUser User { get; set; }
        #endregion
    }
}
