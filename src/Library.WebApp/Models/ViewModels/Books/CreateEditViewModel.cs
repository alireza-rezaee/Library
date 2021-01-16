using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Models.ViewModels.Books
{
    public class CreateEditViewModel
    {
        public Book Book { get; set; }

        public IFormFile BookImage { get; set; }

        public string[] AuthorNames { get; set; }
    }
}
