using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final_Entity.Data;
using Final_Entity.Models;

namespace Final_Entity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChofersController : ControllerBase
    {
        private readonly Final_EntityContext _context;

        public ChofersController(Final_EntityContext context)
        {
            _context = context;
        }

        // GET: api/Chofers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chofer>>> GetChofer()
        {
            return await _context.choferes.ToListAsync();
        }

        // GET: api/Chofers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chofer>> GetChofer(int id)
        {
            var chofer = await _context.choferes.FindAsync(id);

            if (chofer == null)
            {
                return NotFound();
            }

            return chofer;
        }

        // PUT: api/Chofers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChofer(int id, Chofer chofer)
        {
            if (id != chofer.chofer_id)
            {
                return BadRequest();
            }

            _context.Entry(chofer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChoferExists(id))
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

        // POST: api/Chofers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chofer>> PostChofer(Chofer chofer)
        {
            _context.choferes.Add(chofer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChofer", new { id = chofer.chofer_id }, chofer);
        }

        // DELETE: api/Chofers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChofer(int id)
        {
            var chofer = await _context.choferes.FindAsync(id);
            if (chofer == null)
            {
                return NotFound();
            }

            _context.choferes.Remove(chofer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChoferExists(int id)
        {
            return _context.choferes.Any(e => e.chofer_id == id);
        }
    }
}
