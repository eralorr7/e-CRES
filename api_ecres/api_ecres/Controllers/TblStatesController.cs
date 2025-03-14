using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_ecres.Model;

namespace api_ecres.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
    public class TblStatesController : ControllerBase
    {
        private readonly EcresMreContext _context;

        public TblStatesController(EcresMreContext context)
        {
            _context = context;
        }

        // GET: api/TblStates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblState>>> GetTblStates()
        {
          if (_context.TblStates == null)
          {
              return NotFound();
          }
            return await _context.TblStates.ToListAsync();
        }

        // GET: api/TblStates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblState>> GetTblState(int id)
        {
          if (_context.TblStates == null)
          {
              return NotFound();
          }
            var tblState = await _context.TblStates.FindAsync(id);

            if (tblState == null)
            {
                return NotFound();
            }

            return tblState;
        }

        // PUT: api/TblStates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblState(int id, TblState tblState)
        {
            if (id != tblState.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblStateExists(id))
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

        // POST: api/TblStates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblState>> PostTblState(TblState tblState)
        {
          if (_context.TblStates == null)
          {
              return Problem("Entity set 'EcresMreContext.TblStates'  is null.");
          }
            _context.TblStates.Add(tblState);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblState", new { id = tblState.Id }, tblState);
        }

        // DELETE: api/TblStates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblState(int id)
        {
            if (_context.TblStates == null)
            {
                return NotFound();
            }
            var tblState = await _context.TblStates.FindAsync(id);
            if (tblState == null)
            {
                return NotFound();
            }

            _context.TblStates.Remove(tblState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblStateExists(int id)
        {
            return (_context.TblStates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
