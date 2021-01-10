using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} ناشر الزامی است.")]
        [MaxLength(200, ErrorMessage = "{0} ناشر تا سقف {1} کاراکتر طول دارد.")]
        [Display(Name = "نام")]
        public string Title { get; set; }

        #region RelationShips
        public ICollection<Book> Book { get; set; }
        #endregion
    }
}
