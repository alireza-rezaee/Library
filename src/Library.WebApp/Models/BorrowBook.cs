using Mohkazv.Library.WebApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Models
{
    public class BorrowBook
    {
        public int BookId { get; set; }

        public string UserId { get; set; }

        #region Relationships
        public Book Book { get; set; }

        public ApplicationUser User { get; set; }
        #endregion
    }
}
