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
    public class TblMonthsController : ControllerBase
    {
        private readonly EcresMreContext _context;

        public TblMonthsController(EcresMreContext context)
        {
            _context = context;
        }

        // GET: api/TblMonths
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMonth>>> GetTblMonths()
        {
          if (_context.TblMonths == null)
          {
              return NotFound();
          }
            return await _context.TblMonths.ToListAsync();
        }

        // GET: api/TblMonths/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblMonth>> GetTblMonth(int id)
        {
          if (_context.TblMonths == null)
          {
              return NotFound();
          }
            var tblMonth = await _context.TblMonths.FindAsync(id);

            if (tblMonth == null)
            {
                return NotFound();
            }

            return tblMonth;
        }

        // PUT: api/TblMonths/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblMonth(int id, TblMonth tblMonth)
        {
            if (id != tblMonth.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblMonth).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblMonthExists(id))
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

        // POST: api/TblMonths
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblMonth>> PostTblMonth(TblMonth tblMonth)
        {
          if (_context.TblMonths == null)
          {
              return Problem("Entity set 'EcresMreContext.TblMonths'  is null.");
          }
            _context.TblMonths.Add(tblMonth);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblMonth", new { id = tblMonth.Id }, tblMonth);
        }

        // DELETE: api/TblMonths/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblMonth(int id)
        {
            if (_context.TblMonths == null)
            {
                return NotFound();
            }
            var tblMonth = await _context.TblMonths.FindAsync(id);
            if (tblMonth == null)
            {
                return NotFound();
            }

            _context.TblMonths.Remove(tblMonth);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblMonthExists(int id)
        {
            return (_context.TblMonths?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
