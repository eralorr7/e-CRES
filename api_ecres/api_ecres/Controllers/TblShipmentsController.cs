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
    public class TblShipmentsController : ControllerBase
    {
        private readonly EcresMreContext _context;

        public TblShipmentsController(EcresMreContext context)
        {
            _context = context;
        }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<TblShipment>>> GetTblShipments()
    {
      if (_context.TblShipments == null)
      {
        return NotFound();
      }

      var filteredShipments = await _context.TblShipments
                                      .Where(s => s.Status == true)
                                      .ToListAsync();

      return Ok(filteredShipments);
    }


    // GET: api/TblShipments/5
    [HttpGet("{id}")]
        public async Task<ActionResult<TblShipment>> GetTblShipment(int id)
        {
          if (_context.TblShipments == null)
          {
              return NotFound();
          }
            var tblShipment = await _context.TblShipments.FindAsync(id);

            if (tblShipment == null)
            {
                return NotFound();
            }

            return tblShipment;
        }

        // PUT: api/TblShipments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblShipment(int id, TblShipment tblShipment)
        {
            if (id != tblShipment.ShipmentId)
            {
                return BadRequest();
            }

            _context.Entry(tblShipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblShipmentExists(id))
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

        // POST: api/TblShipments
        [HttpPost]
        public async Task<ActionResult<TblShipment>> PostTblShipment(TblShipment tblShipment)
        {
          if (_context.TblShipments == null)
          {
              return Problem("Entity set 'EcresMreContext.TblShipments'  is null.");
          }
            _context.TblShipments.Add(tblShipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblShipment", new { id = tblShipment.ShipmentId }, tblShipment);
        }

        // DELETE: api/TblShipments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblShipment(int id)
        {
            if (_context.TblShipments == null)
            {
                return NotFound();
            }
            var tblShipment = await _context.TblShipments.FindAsync(id);
            if (tblShipment == null)
            {
                return NotFound();
            }

            _context.TblShipments.Remove(tblShipment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblShipmentExists(int id)
        {
            return (_context.TblShipments?.Any(e => e.ShipmentId == id)).GetValueOrDefault();
        }
    }
}
