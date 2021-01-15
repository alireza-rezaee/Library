using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mohkazv.Library.WebApp.Data;
using Mohkazv.Library.WebApp.Models;

namespace Mohkazv.Library.WebApp.Controllers
{
    [Route("books")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context) => _context = context;

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
        public async Task<IActionResult> Create([Bind("Id,Isbn,Name,Description,CoverPath,PublishInformation,TypeId,LanguageId,PublisherId,DdcId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DdcId"] = new SelectList(_context.DeweyDecimalClassifications, "Id", "Id", book.DdcId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Title", book.PublisherId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Title", book.TypeId);
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["DdcId"] = new SelectList(_context.DeweyDecimalClassifications, "Id", "Id", book.DdcId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Title", book.PublisherId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Title", book.TypeId);
            return View(book);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Isbn,Name,Description,CoverPath,PublishInformation,TypeId,LanguageId,PublisherId,DdcId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["DdcId"] = new SelectList(_context.DeweyDecimalClassifications, "Id", "Id", book.DdcId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Title", book.PublisherId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Title", book.TypeId);
            return View(book);
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
