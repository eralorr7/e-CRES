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
    public class TblShipmentTermsController : ControllerBase
    {
        private readonly EcresMreContext _context;

        public TblShipmentTermsController(EcresMreContext context)
        {
            _context = context;
        }

        // GET: api/TblShipmentTerms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblShipmentTerm>>> GetTblShipmentTerms()
        {
          if (_context.TblShipmentTerms == null)
          {
              return NotFound();
          }
            return await _context.TblShipmentTerms.ToListAsync();
        }

        // GET: api/TblShipmentTerms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblShipmentTerm>> GetTblShipmentTerm(int id)
        {
          if (_context.TblShipmentTerms == null)
          {
              return NotFound();
          }
            var tblShipmentTerm = await _context.TblShipmentTerms.FindAsync(id);

            if (tblShipmentTerm == null)
            {
                return NotFound();
            }

            return tblShipmentTerm;
        }

        // PUT: api/TblShipmentTerms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblShipmentTerm(int id, TblShipmentTerm tblShipmentTerm)
        {
            if (id != tblShipmentTerm.ShipmentTermId)
            {
                return BadRequest();
            }

            _context.Entry(tblShipmentTerm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblShipmentTermExists(id))
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

        // POST: api/TblShipmentTerms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblShipmentTerm>> PostTblShipmentTerm(TblShipmentTerm tblShipmentTerm)
        {
          if (_context.TblShipmentTerms == null)
          {
              return Problem("Entity set 'EcresMreContext.TblShipmentTerms'  is null.");
          }
            _context.TblShipmentTerms.Add(tblShipmentTerm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblShipmentTerm", new { id = tblShipmentTerm.ShipmentTermId }, tblShipmentTerm);
        }

        // DELETE: api/TblShipmentTerms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblShipmentTerm(int id)
        {
            if (_context.TblShipmentTerms == null)
            {
                return NotFound();
            }
            var tblShipmentTerm = await _context.TblShipmentTerms.FindAsync(id);
            if (tblShipmentTerm == null)
            {
                return NotFound();
            }

            _context.TblShipmentTerms.Remove(tblShipmentTerm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblShipmentTermExists(int id)
        {
            return (_context.TblShipmentTerms?.Any(e => e.ShipmentTermId == id)).GetValueOrDefault();
        }
    }
}
