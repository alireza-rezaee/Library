using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Models
{
    public class BookAuthor
    {
        public int BookId { get; set; }

        public int AuthorId { get; set; }

        #region Relationships
        public Book Book { get; set; }

        public Author Author { get; set; }
        #endregion
    }
}
