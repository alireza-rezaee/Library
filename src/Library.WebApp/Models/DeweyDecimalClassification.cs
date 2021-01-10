using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Models
{
    public class DeweyDecimalClassification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} رده‌بندی دیویی الزامی است.")]
        [MaxLength(300, ErrorMessage = "{0} رده‌بندی دیویی تا سقف {1} کاراکتر طول دارد.")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        #region RelationShips
        public ICollection<Book> Book { get; set; }
        #endregion
    }
}
