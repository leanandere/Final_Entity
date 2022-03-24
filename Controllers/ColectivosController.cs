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
    public class ColectivosController : ControllerBase
    {
        private readonly Final_EntityContext _context;

        public ColectivosController(Final_EntityContext context)
        {
            _context = context;
        }

        // GET: api/Colectivos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colectivo>>> GetColectivo()
        {
          
            return await _context.colectivos
                .Include(e => e.datos)
                .Include(e => e.estado)
                .ToListAsync();
                
        }

        // GET: api/Colectivos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colectivo>> GetColectivo(int id)
        {
            var colectivo = await _context.colectivos.Where(e => e.colectivo_id == id)
                .Include(i => i.datos)
               .Include(e => e.estado)
                .ToListAsync();

            if (colectivo.Count<1)
            {
                return NotFound();
            }

            return colectivo[0];
        }

        // PUT: api/Colectivos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColectivo(int id, Colectivo colectivo)
        {
            if (id != colectivo.colectivo_id)
            {
                return BadRequest();
            }

            //_context.Entry(colectivo).State = EntityState.Modified;

            _context.colectivos.Update(colectivo);

            try
            {
                await _context.SaveChangesAsync();
                var colectivoeditado = await _context.colectivos.FindAsync(id); 
                return Ok(colectivoeditado);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColectivoExists(id))
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

        // POST: api/Colectivos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Colectivo>> PostColectivo(Colectivo colectivo)
        {
            //colectivo.colectivo_estado = 1;
            _context.colectivos.Add(colectivo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColectivo", new { id = colectivo.colectivo_id }, colectivo);
        }

        // DELETE: api/Colectivos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColectivo(int id)
        {
            var colectivo = await _context.colectivos.FindAsync(id);
            if (colectivo == null)
            {
                return NotFound();
            }
            
            _context.colectivos.Remove(colectivo);

            await _context.SaveChangesAsync();
           
            return NoContent();
        }

        private bool ColectivoExists(int id)
        {
            return _context.colectivos.Any(e => e.colectivo_id == id);
        }
    }
}
