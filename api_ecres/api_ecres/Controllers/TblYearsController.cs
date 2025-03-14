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
    public class TblYearsController : ControllerBase
    {
        private readonly EcresMreContext _context;

        public TblYearsController(EcresMreContext context)
        {
            _context = context;
        }

        // GET: api/TblYears
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblYear>>> GetTblYears()
        {
          if (_context.TblYears == null)
          {
              return NotFound();
          }
            return await _context.TblYears.ToListAsync();
        }

        // GET: api/TblYears/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblYear>> GetTblYear(int id)
        {
          if (_context.TblYears == null)
          {
              return NotFound();
          }
            var tblYear = await _context.TblYears.FindAsync(id);

            if (tblYear == null)
            {
                return NotFound();
            }

            return tblYear;
        }

        // PUT: api/TblYears/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblYear(int id, TblYear tblYear)
        {
            if (id != tblYear.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblYear).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblYearExists(id))
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

        // POST: api/TblYears
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblYear>> PostTblYear(TblYear tblYear)
        {
          if (_context.TblYears == null)
          {
              return Problem("Entity set 'EcresMreContext.TblYears'  is null.");
          }
            _context.TblYears.Add(tblYear);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblYear", new { id = tblYear.Id }, tblYear);
        }

        // DELETE: api/TblYears/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblYear(int id)
        {
            if (_context.TblYears == null)
            {
                return NotFound();
            }
            var tblYear = await _context.TblYears.FindAsync(id);
            if (tblYear == null)
            {
                return NotFound();
            }

            _context.TblYears.Remove(tblYear);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblYearExists(int id)
        {
            return (_context.TblYears?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
