using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Models
{
    public class Language
    {
        [Key]
        [Required(ErrorMessage = "وارد کردن {0} زبان الزامی است.")]
        [MaxLength(50, ErrorMessage = "{0} زبان تا سقف {1} کاراکتر طول دارد.")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        #region RelationShips
        public ICollection<Book> Book { get; set; }
        #endregion
    }
}
