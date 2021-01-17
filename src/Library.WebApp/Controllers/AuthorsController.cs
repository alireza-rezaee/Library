using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mohkazv.Library.WebApp.Data;
using Mohkazv.Library.WebApp.Models;
using Mohkazv.Library.WebApp.Models.ViewModels.Authors;

namespace Mohkazv.Library.WebApp.Controllers
{
    [Route("authors")]
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context) => _context = context;

        [HttpGet("")]
        public async Task<IActionResult> Index()
            => View(await _context.Authors
                .Include(a => a.BookAuthors)
                .ThenInclude(a => a.Author)
                .Select(a => new IndexViewModel { AuthorId = a.Id, AuthorName = a.FullName, BookCounts = a.BookAuthors.Count })
                .ToListAsync());

        [HttpGet("create")]
        public IActionResult Create() => View();

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var author = await _context.Authors.FindAsync(id);

            if (author == null)
                return NotFound();

            return View(author);
        }

        [HttpPost("edit/{id}")]
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName")] Author modelAuthor)
        {
            if (id != modelAuthor.Id)
                return NotFound();

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                author.FullName = modelAuthor.FullName;

                _context.Update(author);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(modelAuthor);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var author = await _context.Authors
                .FindAsync(id);

            if (author == null)
                return NotFound();

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
                return NotFound();

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("suggestions")]
        public async Task<List<string>> Suggestions(string q)
        {
            if (string.IsNullOrEmpty(q)) return null;
            return await _context.Authors.Where(a => a.FullName.Contains(q)).Take(10).Select(a => a.FullName).ToListAsync();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
