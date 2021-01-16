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
    [Route("languages")]
    public class LanguagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LanguagesController(ApplicationDbContext context)
            => _context = context;

        [HttpGet("")]
        public async Task<IActionResult> Index()
            => View(await _context.Languages.ToListAsync());

        [HttpGet("create")]
        public IActionResult Create()
            => View();

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Language language)
        {
            if (ModelState.IsValid)
            {
                _context.Add(language);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var language = await _context.Languages.FindAsync(id);

            if (language == null)
                return NotFound();

            return View(language);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name", "Id")] Language languageModel)
        {
            if (id != languageModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var language = await _context.Languages.FindAsync(id);

                if (language == null)
                    return NotFound();

                language.Name = languageModel.Name;

                _context.Update(language);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(languageModel);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var language = await _context.Languages.FindAsync(id);

            if (language == null)
                return NotFound();

            return View(language);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var language = await _context.Languages.FindAsync(id);

            if (language == null)
                return NotFound();

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanguageExists(int id)
            => _context.Languages.Any(e => e.Id == id);
    }
}
