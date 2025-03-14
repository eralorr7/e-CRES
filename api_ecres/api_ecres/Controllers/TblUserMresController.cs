using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_ecres.Model;
using api_ecres.DTOs;

namespace api_ecres.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
    public class TblUserMresController : ControllerBase
    {
        private readonly EcresMreContext _context;

        public TblUserMresController(EcresMreContext context)
        {
            _context = context;
        }

        // GET: api/TblUserMres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUserMre>>> GetTblUserMres()
        {
          if (_context.TblUserMres == null)
          {
              return NotFound();
          }
            return await _context.TblUserMres.ToListAsync();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> PostLogin([FromBody] LoginDTO tblUserMre)
        {
          var existingUser = await _context.TblUserMres.SingleOrDefaultAsync(u => u.Username == tblUserMre.Username && u.Password == tblUserMre.Password);

          if (existingUser == null)
          {
            return BadRequest("Invalid username or password");
          }

          // User is authenticated, you can generate a token or set a session/cookie here

          return Ok(existingUser);

        }



    // GET: api/TblUserMres/5
    [HttpGet("{id}")]
        public async Task<ActionResult<TblUserMre>> GetTblUserMre(int id)
        {
          if (_context.TblUserMres == null)
          {
              return NotFound();
          }
            var tblUserMre = await _context.TblUserMres.FindAsync(id);

            if (tblUserMre == null)
            {
                return NotFound();
            }

            return tblUserMre;
        }

        // PUT: api/TblUserMres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUserMre(int id, TblUserMre tblUserMre)
        {
            if (id != tblUserMre.UserId)
            {
                return BadRequest();
            }

            _context.Entry(tblUserMre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUserMreExists(id))
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

        // POST: api/TblUserMres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblUserMre>> PostTblUserMre(TblUserMre tblUserMre)
        {
          if (_context.TblUserMres == null)
          {
              return Problem("Entity set 'EcresMreContext.TblUserMres'  is null.");
          }
            _context.TblUserMres.Add(tblUserMre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUserMre", new { id = tblUserMre.UserId }, tblUserMre);
        }

        // DELETE: api/TblUserMres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUserMre(int id)
        {
            if (_context.TblUserMres == null)
            {
                return NotFound();
            }
            var tblUserMre = await _context.TblUserMres.FindAsync(id);
            if (tblUserMre == null)
            {
                return NotFound();
            }

            _context.TblUserMres.Remove(tblUserMre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblUserMreExists(int id)
        {
            return (_context.TblUserMres?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
