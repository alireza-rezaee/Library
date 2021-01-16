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
    [Route("publishers")]
    public class PublishersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublishersController(ApplicationDbContext context)
            => _context = context;

        [HttpGet("")]
        public async Task<IActionResult> Index()
            => View(await _context.Publishers.ToListAsync());

        [HttpGet("create")]
        public IActionResult Create()
            => View();

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
                return NotFound();

            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] Publisher publisherModel)
        {
            if (id != publisherModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var publisher = await _context.Publishers.FindAsync(id);

                if (publisher == null)
                    return NotFound();

                _context.Update(publisher);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(publisherModel);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (publisher == null)
                return NotFound();

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
                return NotFound();

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
            => _context.Publishers.Any(e => e.Id == id);
    }
}
