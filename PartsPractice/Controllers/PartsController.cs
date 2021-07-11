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
        public async Task<ActionResult<IEnumerable<PartsDTO>>> GetPartsItems()
        {
            // return await _context.PartsItems.ToListAsync();
            return await _context.PartsItems
                .Select(x => PartsToDTO(x))                //Get parts to DTO 
                .ToListAsync();
        }

        // GET: api/Parts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartsDTO>> GetPart(long id)
        {
            var part = await _context.PartsItems.FindAsync(id);     // Get part from DB

            if (part == null)
            {
                return NotFound();
            }

            return PartsToDTO(part);                    // copy parts to partDTO and return
        }

        // PUT: api/Parts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParts(long id, PartsDTO partDTO)     
        {
            if (id != partDTO.PartId)
            {
                return BadRequest();
            }

            var part = await _context.PartsItems.FindAsync(id);
            if (part == null)
            {
                return NotFound();
            }

            part.Available = partDTO.Available;
            part.Description = partDTO.Description;
            part.Name = partDTO.Name;
            part.Price = partDTO.Price;
            part.QtyOnHand = partDTO.QtyOnHand;
            part.PartId = partDTO.PartId;


            _context.Entry(partDTO).State = EntityState.Modified;       // Change state to modified

            try
            {
                await _context.SaveChangesAsync();                      // Save the changes
            }
            catch (DbUpdateConcurrencyException) when (!PartsExists(id))
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
        public async Task<ActionResult<PartsDTO>> PostParts(PartsDTO partDTO)
        {            
            var part = new Parts
            {
                Available = partDTO.Available,
                Description = partDTO.Description,
                Name = partDTO.Name,
                Price = partDTO.Price,
                QtyOnHand = partDTO.QtyOnHand,
                PartId = partDTO.PartId               
            };
                        
            _context.PartsItems.Add(part);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetParts", new { id = parts.PartId }, parts);
            
            
            
            return CreatedAtAction(nameof(GetPart), new { id = part.PartId }, PartsToDTO(part));

            // use nameof instead of hardcode to protect against refactor error
            // CreatedAtAction produces a Status201Created response 
            // returns newly created resource/parts
            // add a location header to the response. the location header specifies the
            // URI of the newly created parts object
            // The url should be the url at which a GET request would return the object url. 
        }

        // DELETE: api/Parts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParts(long id)
        {
            var part = await _context.PartsItems.FindAsync(id);
            if (part == null)
            {
                return NotFound();
            }

            _context.PartsItems.Remove(part);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartsExists(long id)
        {
            return _context.PartsItems.Any(e => e.PartId == id);
        }

        // copy parts to parts DTO. Pass Parts Object into method
        private static PartsDTO PartsToDTO(Parts parts) =>
            new PartsDTO
            {
                PartId      =   parts.PartId,
                Name        =   parts.Name,
                Description =   parts.Description,
                Price       =   parts.Price,
                QtyOnHand   =   parts.QtyOnHand,
                Available   =   parts.Available


            };

    }
}
