using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PartsPractice.Models;
using PartsPractice.Models.DataLayer;

namespace PartsPractice.Controllers
{
    public class PartsDisplayController : Controller
    {
        private readonly AppDbContext _context;

        public PartsDisplayController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PartsDisplay
        [Route("[controller]")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.PartsItems.ToListAsync());
        }

        // GET: PartsDisplay/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parts = await _context.PartsItems
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (parts == null)
            {
                return NotFound();
            }

            return View(parts);
        }

        // GET: PartsDisplay/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PartsDisplay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartId,Name,Description,QtyOnHand,Price,Available,WholeSalePrice")] Parts parts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parts);
        }

        // GET: PartsDisplay/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parts = await _context.PartsItems.FindAsync(id);
            if (parts == null)
            {
                return NotFound();
            }
            return View(parts);
        }

        // POST: PartsDisplay/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PartId,Name,Description,QtyOnHand,Price,Available,WholeSalePrice")] Parts parts)
        {
            if (id != parts.PartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartsExists(parts.PartId))
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
            return View(parts);
        }

        // GET: PartsDisplay/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parts = await _context.PartsItems
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (parts == null)
            {
                return NotFound();
            }

            return View(parts);
        }

        // POST: PartsDisplay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var parts = await _context.PartsItems.FindAsync(id);
            _context.PartsItems.Remove(parts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartsExists(long id)
        {
            return _context.PartsItems.Any(e => e.PartId == id);
        }
    }
}
