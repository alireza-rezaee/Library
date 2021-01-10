using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Models
{
    public class Type
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} ماده الزامی است.")]
        [MaxLength(50, ErrorMessage = "{0} ماده تا سقف {1} کاراکتر طول دارد.")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        #region RelationShips
        public ICollection<Book> Book { get; set; }
        #endregion
    }
}
