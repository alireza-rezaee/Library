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
    public class BorrowBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BorrowBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BorrowBooks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BorrowBooks.Include(b => b.Book).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BorrowBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowBook = await _context.BorrowBooks
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (borrowBook == null)
            {
                return NotFound();
            }

            return View(borrowBook);
        }

        // GET: BorrowBooks/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BorrowBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,UserId,DeliveryDateTime,ReturnDateTime,ReturnedDateTime")] BorrowBook borrowBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", borrowBook.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", borrowBook.UserId);
            return View(borrowBook);
        }

        // GET: BorrowBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowBook = await _context.BorrowBooks.FindAsync(id);
            if (borrowBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", borrowBook.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", borrowBook.UserId);
            return View(borrowBook);
        }

        // POST: BorrowBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,UserId,DeliveryDateTime,ReturnDateTime,ReturnedDateTime")] BorrowBook borrowBook)
        {
            if (id != borrowBook.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowBookExists(borrowBook.BookId))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", borrowBook.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", borrowBook.UserId);
            return View(borrowBook);
        }

        // GET: BorrowBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowBook = await _context.BorrowBooks
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (borrowBook == null)
            {
                return NotFound();
            }

            return View(borrowBook);
        }

        // POST: BorrowBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowBook = await _context.BorrowBooks.FindAsync(id);
            _context.BorrowBooks.Remove(borrowBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowBookExists(int id)
        {
            return _context.BorrowBooks.Any(e => e.BookId == id);
        }
    }
}
