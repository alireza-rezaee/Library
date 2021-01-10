using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} نویسنده الزامی است.")]
        [MaxLength(50, ErrorMessage = "{0} نویسنده تا سقف {1} کاراکتر طول دارد.")]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} نویسنده الزامی است.")]
        [MaxLength(50, ErrorMessage = "{0} نویسنده تا سقف {1} کاراکتر طول دارد.")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }

        #region RelationShips
        public ICollection<BookAuthor> BookAuthors { get; set; }
        #endregion
    }
}
