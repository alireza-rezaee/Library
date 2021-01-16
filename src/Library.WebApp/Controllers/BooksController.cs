using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
            if (id == null)
                return NotFound();

            var book = await _context.Books
                .Include(b => b.DeweyDecimalClassification)
                .Include(b => b.Language)
                .Include(b => b.Publisher)
                .Include(b => b.Type)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
                return NotFound();

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

                // START::Authors
                if (vm.AuthorNames != null)
                {
                    vm.AuthorNames = vm.AuthorNames.Select(an => an.Trim()).Where(an => !string.IsNullOrEmpty(an)).ToArray();

                    if (vm.AuthorNames != null && vm.AuthorNames.Any())
                    {
                        var authors = await _context.Authors.Where(author => vm.AuthorNames.Contains(author.FullName)).ToListAsync();

                        foreach (var authorName in vm.AuthorNames)
                            if (!authors.Any(a => a.FullName == authorName))
                                authors.Add(new Author { FullName = authorName });

                        vm.Book.BookAuthors = new List<BookAuthor>();
                        foreach (var author in authors)
                            vm.Book.BookAuthors.Add(new BookAuthor { Book = vm.Book, Author = author });
                    }
                }
                // END::Authors

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
                return NotFound();

            var book = await _context.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return NotFound();

            ViewData["DdcId"] = new SelectList(_context.DeweyDecimalClassifications, "Id", "Id", book.DdcId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Title", book.PublisherId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Title", book.TypeId);

            return View(new CreateEditViewModel { Book = book, AuthorNames = book.BookAuthors.Select(ba => ba.Author.FullName).ToArray() });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CreateEditViewModel vm)
        {
            if (id == null)
                return NotFound();

            var book = await _context.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

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

                    #region Edit Authors of the inprocess book (Purpose: Add, insert or remove from «BookAuthors» table/entity)
                    // Target DB Table is BookAuthors

                    // 1.   DELETE
                    // 1.1      - DELETE 'BookAuthors' records that belong to former authors who are no longer the authors of this book
                    vm.AuthorNames ??= Array.Empty<string>();
                    book.BookAuthors ??= new List<BookAuthor>();
                    var oldAuthorsThatNotBeAsThisBookAuthorAnyMore = book.BookAuthors.Where(ba => !vm.AuthorNames.Contains(ba.Author.FullName)).ToList();
                    foreach (var bookAuthor in oldAuthorsThatNotBeAsThisBookAuthorAnyMore ?? Enumerable.Empty<BookAuthor>())
                        book.BookAuthors.Remove(bookAuthor);

                    // 2.   INSERT
                    // 2.1      - INSERT 'BookAuthors' records of new authors already on the table
                    var dbExistAuthors = await _context.Authors.Where(a => vm.AuthorNames.Contains(a.FullName)).ToListAsync();
                    dbExistAuthors ??= new List<Author>();
                    var newDbExistAuthors = dbExistAuthors?.Where(a => !book.BookAuthors.Select(ba => ba.Author).Contains(a));
                    foreach (var author in newDbExistAuthors ?? Enumerable.Empty<Author>())
                        book.BookAuthors.Add(new BookAuthor { Book = book, Author = author });

                    // 2.1      - INSERT 'BookAuthors' records related to new authors who were not previously listed as authors
                    var newNonDbExistAuthors = vm.AuthorNames.Where(an => !dbExistAuthors.Select(a => a.FullName).Contains(an));
                    foreach (var author in newNonDbExistAuthors ?? Enumerable.Empty<string>())
                        book.BookAuthors.Add(new BookAuthor { Book = book, Author = new Author { FullName = author } });
                    #endregion Edit Authors of the inprocess book (Purpose: Add, insert or remove from «BookAuthors» table/entity)

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
                return NotFound();

            var book = await _context.Books
                .Include(b => b.DeweyDecimalClassification)
                .Include(b => b.Language)
                .Include(b => b.Publisher)
                .Include(b => b.Type)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

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
                (book.BookAuthors != null && book.BookAuthors.Any(bookAuthors => bookAuthors.Author != null && (!string.IsNullOrEmpty(bookAuthors.Author.FullName) && bookAuthors.Author.FullName.Contains(q)))))
                .ToListAsync());
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
