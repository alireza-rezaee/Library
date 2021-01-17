using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Models.ViewModels.Authors
{
    public class IndexViewModel
    {
        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public int BookCounts { get; set; }
    }
}
