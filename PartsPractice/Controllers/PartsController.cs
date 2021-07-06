using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartsPractice.Models;
using PartsPractice.Models.DataLayer;

namespace PartsPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PartsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Parts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parts>>> GetPartsItems()
        {
            return await _context.PartsItems.ToListAsync();
        }

        // GET: api/Parts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parts>> GetParts(long id)
        {
            var parts = await _context.PartsItems.FindAsync(id);

            if (parts == null)
            {
                return NotFound();
            }

            return parts;
        }

        // PUT: api/Parts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParts(long id, Parts parts)
        {
            if (id != parts.PartId)
            {
                return BadRequest();
            }

            _context.Entry(parts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Parts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Parts>> PostParts(Parts parts)
        {
            _context.PartsItems.Add(parts);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetParts", new { id = parts.PartId }, parts);
            return CreatedAtAction(nameof(GetParts), new { id = parts.PartId }, parts);
        }

        // DELETE: api/Parts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParts(long id)
        {
            var parts = await _context.PartsItems.FindAsync(id);
            if (parts == null)
            {
                return NotFound();
            }

            _context.PartsItems.Remove(parts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartsExists(long id)
        {
            return _context.PartsItems.Any(e => e.PartId == id);
        }
    }
}
