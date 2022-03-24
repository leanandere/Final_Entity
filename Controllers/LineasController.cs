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
    public class LineasController : ControllerBase
    {
        private readonly Final_EntityContext _context;

        public LineasController(Final_EntityContext context)
        {
            _context = context;
        }

        // GET: api/Lineas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Linea>>> GetLinea()
        {
            return await _context.lineas.ToListAsync();
        }

        // GET: api/Lineas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Linea>> GetLinea(int id)
        {
            var linea = await _context.lineas.FindAsync(id);

            if (linea == null)
            {
                return NotFound();
            }

            return linea;
        }

        // PUT: api/Lineas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLinea(int id, Linea linea)
        {
            if (id != linea.linea_id)
            {
                return BadRequest();
            }

            _context.Entry(linea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineaExists(id))
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

        // POST: api/Lineas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Linea>> PostLinea(Linea linea)
        {
            _context.lineas.Add(linea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLinea", new { id = linea.linea_id }, linea);
        }

        // DELETE: api/Lineas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLinea(int id)
        {
            var linea = await _context.lineas.FindAsync(id);
            if (linea == null)
            {
                return NotFound();
            }

            _context.lineas.Remove(linea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LineaExists(int id)
        {
            return _context.lineas.Any(e => e.linea_id == id);
        }
    }
}
