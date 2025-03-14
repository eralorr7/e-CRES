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
    public class TblRubberTypesController : ControllerBase
    {
        private readonly EcresMreContext _context;

        public TblRubberTypesController(EcresMreContext context)
        {
            _context = context;
        }

        // GET: api/TblRubberTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblRubberType>>> GetTblRubberTypes()
        {
          if (_context.TblRubberTypes == null)
          {
              return NotFound();
          }
            return await _context.TblRubberTypes.ToListAsync();
        }

        // GET: api/TblRubberTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblRubberType>> GetTblRubberType(int id)
        {
          if (_context.TblRubberTypes == null)
          {
              return NotFound();
          }
            var tblRubberType = await _context.TblRubberTypes.FindAsync(id);

            if (tblRubberType == null)
            {
                return NotFound();
            }

            return tblRubberType;
        }

    // PUT: api/TblRubberTypes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTblRubberType(int id, TblRubberType tblRubberType)
    {
      if (id != tblRubberType.RubberId)
      {
        return BadRequest();
      }

      _context.Entry(tblRubberType).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TblRubberTypeExists(id))
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

    // POST: api/TblRubberTypes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
        public async Task<ActionResult<TblRubberType>> PostTblRubberType(TblRubberType tblRubberType)
        {
          if (_context.TblRubberTypes == null)
          {
              return Problem("Entity set 'EcresMreContext.TblRubberTypes'  is null.");
          }
            _context.TblRubberTypes.Add(tblRubberType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblRubberType", new { id = tblRubberType.RubberId }, tblRubberType);
        }

        // DELETE: api/TblRubberTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblRubberType(int id)
        {
            if (_context.TblRubberTypes == null)
            {
                return NotFound();
            }
            var tblRubberType = await _context.TblRubberTypes.FindAsync(id);
            if (tblRubberType == null)
            {
                return NotFound();
            }

            _context.TblRubberTypes.Remove(tblRubberType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblRubberTypeExists(int id)
        {
            return (_context.TblRubberTypes?.Any(e => e.RubberId == id)).GetValueOrDefault();
        }
    }
}
