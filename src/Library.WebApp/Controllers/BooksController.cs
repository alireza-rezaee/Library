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
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context) => _context = context;

        // GET: Books
        public async Task<IActionResult> Index()
            => View(await _context.Books.Include(b => b.DeweyDecimalClassification).Include(b => b.Language).Include(b => b.Publisher).Include(b => b.Type).ToListAsync());

        // GET: Books/Details/5
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

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["DdcId"] = new SelectList(_context.DeweyDecimalClassifications, "Id", "Id");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Title");
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Title");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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

        // GET: Books/Edit/5
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

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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

        // GET: Books/Delete/5
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

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
