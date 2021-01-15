using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mohkazv.Library.WebApp.Data;
using Mohkazv.Library.WebApp.Extensions;
using Mohkazv.Library.WebApp.Helpers;
using Mohkazv.Library.WebApp.Models;
using Mohkazv.Library.WebApp.Models.ViewModels.Books;

namespace Mohkazv.Library.WebApp.Controllers
{
    [Route("books")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileManager _ifileManager;
        private readonly IWebHostEnvironment _env;

        public BooksController(ApplicationDbContext context, IFileManager ifileManager, IWebHostEnvironment env)
            => (_context, _ifileManager, _env) = (context, ifileManager, env);

        [HttpGet("")]
        public async Task<IActionResult> Index()
            => View(await _context.Books.Include(b => b.DeweyDecimalClassification).Include(b => b.Language).Include(b => b.Publisher).Include(b => b.Type).ToListAsync());

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.DeweyDecimalClassification)
                .Include(b => b.Language)
                .Include(b => b.Publisher)
                .Include(b => b.Type)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewData["DdcId"] = new SelectList(_context.DeweyDecimalClassifications, "Id", "Id");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Title");
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Title");
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.BookImage != null)
                {
                    var persianDateTime = PersianDateTime.Now;
                    var dateTime = persianDateTime.ToDateTime();

                    // 2MiB = 2097152 bytes
                    vm.BookImage.Check(2097152, new string[] { "image/jpg", "image/jpeg", "image/png", "image/gif" });

                    var imagePath = $"uploads/images/{persianDateTime.ToString("yyyy/MM/dd/yyyyMMddhhmmss") + DateTime.Now.ToString("ffff") + new Random().Next(1000000, 9999999)}_{Helpers.File.ValidateName(vm.BookImage.FileName)}";

                    await _ifileManager.SaveFile(vm.BookImage, imagePath);

                    vm.Book.CoverPath = $"/{imagePath}";
                }

                _context.Add(vm.Book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DdcId"] = new SelectList(_context.DeweyDecimalClassifications, "Id", "Id", vm.Book.DdcId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", vm.Book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Title", vm.Book.PublisherId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Title", vm.Book.TypeId);
            return View(vm);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            ViewData["DdcId"] = new SelectList(_context.DeweyDecimalClassifications, "Id", "Id", book.DdcId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Title", book.PublisherId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Title", book.TypeId);

            return View(new CreateEditViewModel { Book = book });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CreateEditViewModel vm)
        {
            if (id == null)
                return NotFound();

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    book.Name = vm.Book.Name;
                    book.Isbn = vm.Book.Isbn;
                    book.Description = vm.Book.Description;
                    book.PublishInformation = vm.Book.PublishInformation;
                    book.TypeId = vm.Book.TypeId;
                    book.DdcId = vm.Book.DdcId;
                    book.PublisherId = vm.Book.PublisherId;
                    book.LanguageId = vm.Book.LanguageId;

                    if (vm.BookImage != null)
                    {
                        var persianDateTime = PersianDateTime.Now;
                        var dateTime = persianDateTime.ToDateTime();

                        // 2MiB = 2097152 bytes
                        vm.BookImage.Check(2097152, new string[] { "image/jpg", "image/jpeg", "image/png", "image/gif" });

                        var imagePath = $"uploads/images/{persianDateTime.ToString("yyyy/MM/dd/yyyyMMddhhmmss") + DateTime.Now.ToString("ffff") + new Random().Next(1000000, 9999999)}_{Helpers.File.ValidateName(vm.BookImage.FileName)}";

                        await _ifileManager.SaveFile(vm.BookImage, imagePath);

                        book.CoverPath = $"/{imagePath}";
                    }

                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(vm.Book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DdcId"] = new SelectList(_context.DeweyDecimalClassifications, "Id", "Id", vm.Book.DdcId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", vm.Book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Title", vm.Book.PublisherId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Title", vm.Book.TypeId);
            return View(vm);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.DeweyDecimalClassification)
                .Include(b => b.Language)
                .Include(b => b.Publisher)
                .Include(b => b.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);

            _ifileManager.DeleteFile(book.CoverPath);

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("search")]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost("search"), ActionName("Search")]
        public async Task<IActionResult> SearchAsk(string q, bool byAuthors = true, bool byPub = true, bool byLang = true, bool byDdc = true)
        {
            var books = _context.Books.AsQueryable();
            var result = new List<Book>();

            if (byPub) books = books.Include(book => book.Publisher);
            if (byLang) books = books.Include(book => book.Language);
            if (byDdc) books = books.Include(book => book.DeweyDecimalClassification);
            if (byAuthors) books = books.Include(book => book.BookAuthors).ThenInclude(bookAuthor => bookAuthor.Author);

            ViewData["searchedQuery"] = q;

            return View("SearchAsk", await books
                .Where(book =>
                (!string.IsNullOrEmpty(book.Name) && book.Name.Contains(q)) ||
                (!string.IsNullOrEmpty(book.Isbn) && book.Isbn.Contains(q)) ||
                (!string.IsNullOrEmpty(book.Description) && book.Description.Contains(q)) ||
                (!string.IsNullOrEmpty(book.PublishInformation) && book.PublishInformation.Contains(q)) ||
                (book.Publisher != null && !string.IsNullOrEmpty(book.Publisher.Title) && book.Publisher.Title.Contains(q)) ||
                (book.Language != null && !string.IsNullOrEmpty(book.Language.Name) && book.Language.Name.Contains(q)) ||
                (book.DeweyDecimalClassification != null && !string.IsNullOrEmpty(book.DeweyDecimalClassification.Title) && book.DeweyDecimalClassification.Title.Contains(q)) ||
                (book.BookAuthors != null && book.BookAuthors.Any(bookAuthors => bookAuthors.Author != null && (!string.IsNullOrEmpty(bookAuthors.Author.FirstName) && bookAuthors.Author.FirstName.Contains(q)) || (!string.IsNullOrEmpty(bookAuthors.Author.LastName) && bookAuthors.Author.LastName.Contains(q)))))
                .ToListAsync());
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
