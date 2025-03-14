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
    public class TblContractTypesController : ControllerBase
    {
        private readonly EcresMreContext _context;

        public TblContractTypesController(EcresMreContext context)
        {
            _context = context;
        }

        // GET: api/TblContractTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContractType>>> GetTblContractTypes()
        {
          if (_context.TblContractTypes == null)
          {
              return NotFound();
          }
            return await _context.TblContractTypes.ToListAsync();
        }

        // GET: api/TblContractTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContractType>> GetTblContractType(int id)
        {
          if (_context.TblContractTypes == null)
          {
              return NotFound();
          }
            var tblContractType = await _context.TblContractTypes.FindAsync(id);

            if (tblContractType == null)
            {
                return NotFound();
            }

            return tblContractType;
        }

        // PUT: api/TblContractTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblContractType(int id, TblContractType tblContractType)
        {
            if (id != tblContractType.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblContractType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblContractTypeExists(id))
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

        // POST: api/TblContractTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblContractType>> PostTblContractType(TblContractType tblContractType)
        {
          if (_context.TblContractTypes == null)
          {
              return Problem("Entity set 'EcresMreContext.TblContractTypes'  is null.");
          }
            _context.TblContractTypes.Add(tblContractType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblContractType", new { id = tblContractType.Id }, tblContractType);
        }

        // DELETE: api/TblContractTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblContractType(int id)
        {
            if (_context.TblContractTypes == null)
            {
                return NotFound();
            }
            var tblContractType = await _context.TblContractTypes.FindAsync(id);
            if (tblContractType == null)
            {
                return NotFound();
            }

            _context.TblContractTypes.Remove(tblContractType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblContractTypeExists(int id)
        {
            return (_context.TblContractTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
