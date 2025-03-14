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
    public class TblUploadUserManualsController : ControllerBase
    {
        private readonly EcresMreContext _context;

        public TblUploadUserManualsController(EcresMreContext context)
        {
            _context = context;
        }


    [HttpPost("{id}")]
    public async Task<IActionResult> UploadUserManual([FromRoute] int id, IFormFile file)
    {
      var details = await _context.TblUploadUserManuals.FirstOrDefaultAsync(n => n.Id == id);
      if (details == null)
      {
        return BadRequest("No notice found.");
      }

      if (file == null || file.Length == 0)
      {
        return BadRequest("No file was provided.");
      }

      //var fileExtension = Path.GetExtension(file.FileName);
      var fileName = $"{id}_{details.UserManual}";
      var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\userManual", fileName);

      try
      {
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
          await file.CopyToAsync(fileStream);
        }
        return Ok();
      }
      catch (Exception ex)
      {
        // Log the exception (ex) here if logging is set up in your project
        return StatusCode(500, "An error occurred while uploading the file.");
      }


      }

      // GET: api/TblUploadUserManuals
      [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUploadUserManual>>> GetTblUploadUserManuals()
        {
          if (_context.TblUploadUserManuals == null)
          {
              return NotFound();
          }
            return await _context.TblUploadUserManuals.ToListAsync();
        }

        // GET: api/TblUploadUserManuals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUploadUserManual>> GetTblUploadUserManual(int id)
        {
          if (_context.TblUploadUserManuals == null)
          {
              return NotFound();
          }
            var tblUploadUserManual = await _context.TblUploadUserManuals.FindAsync(id);

            if (tblUploadUserManual == null)
            {
                return NotFound();
            }

            return tblUploadUserManual;
        }

        // PUT: api/TblUploadUserManuals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUploadUserManual(int id, TblUploadUserManual tblUploadUserManual)
        {
            if (id != tblUploadUserManual.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblUploadUserManual).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUploadUserManualExists(id))
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

        // POST: api/TblUploadUserManuals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754



        [HttpPost]
        public async Task<ActionResult<TblUploadUserManual>> PostTblUploadUserManual(TblUploadUserManual tblUploadUserManual)
        {
          if (_context.TblUploadUserManuals == null)
          {
              return Problem("Entity set 'EcresMreContext.TblUploadUserManuals'  is null.");
          }
            _context.TblUploadUserManuals.Add(tblUploadUserManual);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUploadUserManual", new { id = tblUploadUserManual.Id }, tblUploadUserManual);
        }

        // DELETE: api/TblUploadUserManuals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUploadUserManual(int id)
        {
            if (_context.TblUploadUserManuals == null)
            {
                return NotFound();
            }
            var tblUploadUserManual = await _context.TblUploadUserManuals.FindAsync(id);
            if (tblUploadUserManual == null)
            {
                return NotFound();
            }

            _context.TblUploadUserManuals.Remove(tblUploadUserManual);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblUploadUserManualExists(int id)
        {
            return (_context.TblUploadUserManuals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
