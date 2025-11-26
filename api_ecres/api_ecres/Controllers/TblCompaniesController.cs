using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_ecres.Model;
using api_ecres.DTOs;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;

namespace api_ecres.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
    public class TblCompaniesController : ControllerBase
    {
        private readonly EcresMreContext _context;

        public TblCompaniesController(EcresMreContext context)
        {
            _context = context;
        }

        // GET: api/TblCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCompany>>> GetTblCompanies()
        {
          if (_context.TblCompanies == null)
          {
              return NotFound();
          }
              return await _context.TblCompanies.ToListAsync();
          }

      // GET: api/TblCompanies/5
      [HttpGet("{id}")]
      public async Task<ActionResult<TblCompany>> GetTblCompany(int id)
      {
        if (_context.TblCompanies == null)
        {
          return NotFound();
        }
        var tblCompany = await _context.TblCompanies.FindAsync(id);

        if (tblCompany == null)
        {
          return NotFound();
        }

        return tblCompany;
      }

      [HttpGet("{username}")]
        public async Task<ActionResult<TblCompany>> GetTblCompanyByUsername(string username)
        {
          if (_context.TblCompanies == null)
          {
            return NotFound();
          }

          // Use FirstOrDefaultAsync to find the company by username
          var tblCompany = await _context.TblCompanies
              .FirstOrDefaultAsync(c => c.Username == username); // Assuming "Username" is the property name

          if (tblCompany == null)
          {
            return NotFound();
          }

          return tblCompany;
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult<TblCompany>> GetTblCompanyByCompanyId(int companyId)
        {
          if (_context.TblCompanies == null)
          {
            return NotFound();
          }

          // Use FirstOrDefaultAsync to find the company by company ID
          var tblCompany = await _context.TblCompanies
              .FirstOrDefaultAsync(c => c.CompanyId == companyId); // Assuming "CompanyId" is the property name

          if (tblCompany == null)
          {
            return NotFound();
          }

          return tblCompany;
        }


        [HttpPost("Login")]
            public async Task<IActionResult> PostLogin([FromBody] LoginDTO tblCompany)
            {
              var existingUser = await _context.TblCompanies.SingleOrDefaultAsync(u => u.Username == tblCompany.Username && u.Password == tblCompany.Password);

              if (existingUser == null)
              {
                return BadRequest("Invalid username or password");
              }

              return Ok(existingUser);
      
            }


        [HttpPost("Login")]
        public async Task<IActionResult> PostLoginEcres([FromBody] LoginDTO loginDTO)
        {
          // Retrieve user with matching username
          var existingUser = await _context.TblCompanies.SingleOrDefaultAsync(u => u.Username == loginDTO.Username);

          if (existingUser == null)
          {
            return BadRequest("Invalid username or password");
          }

          // Use a secure hashing library to verify the password
          bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDTO.Password, existingUser.Password);

          if (!isPasswordValid)
          {
            return BadRequest("Invalid username or password");
          }

          // Password matches - proceed with login
          return Ok(existingUser);
        }



        [HttpPost("Login")]
        public async Task<IActionResult> PostLogin1([FromBody] LoginDTO loginDTO)
        {
          // Retrieve user with matching username
          var existingUser = await _context.TblCompanies.SingleOrDefaultAsync(u => u.Username == loginDTO.Username);

          // If no user is found, return an error
          if (existingUser == null)
          {
            return BadRequest("Invalid username or password");
          }

          // Verify if the entered password matches the hashed password stored in the database
          bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDTO.Password, existingUser.Password);

          // If password is invalid, return an error
          if (!isPasswordValid)
          {
            return BadRequest("Invalid username or password");
          }

          // Password matches - proceed with login, return the user details (or other info)
          return Ok(existingUser);
        }



        [HttpPost("ChangePassword")]
            public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
            {
              var existingUser = await _context.TblCompanies.SingleOrDefaultAsync(u => u.Username == changePasswordDTO.Username);

              if (existingUser == null || existingUser.Password != changePasswordDTO.OldPassword)
              {
                return BadRequest(new { message = "Invalid username or old password." }); // Ensure this returns a proper error structure
              }

              if (changePasswordDTO.NewPassword.Length < 8)
              {
                return BadRequest(new { message = "New password must be at least 8 characters long." });
              }

              existingUser.Password = changePasswordDTO.NewPassword;
              await _context.SaveChangesAsync();

              return Ok(new { success = true, message = "Password changed successfully!" }); // Ensure this returns a proper success structure
            }


        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePasswordEcres([FromBody] ChangePasswordDTO changePasswordDTO)
        {
          var existingUser = await _context.TblCompanies.SingleOrDefaultAsync(u => u.Username == changePasswordDTO.Username);

          if (existingUser == null || !BCrypt.Net.BCrypt.Verify(changePasswordDTO.OldPassword, existingUser.Password))
          {
            return BadRequest(new { message = "Invalid username or old password." });
          }

          if (changePasswordDTO.NewPassword.Length < 8)
          {
            return BadRequest(new { message = "New password must be at least 8 characters long." });
          }

          // Hash the new password before saving
          existingUser.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDTO.NewPassword);
          await _context.SaveChangesAsync();

          return Ok(new { success = true, message = "Password changed successfully!" });
        }




      [HttpGet("getAllEmailCompanies")]
          public async Task<IActionResult> GetAllEmailCompanies()
          {
            var companies = await _context.TblCompanies
                .Where(x => x.Status == true)
                .Select(x => new
                {
                  companyId = x.CompanyId,
                  email1 = x.Email1
                })
                .ToListAsync();

            return Ok(companies);
          }



      [HttpPut("{companyId}")]
        public async Task<IActionResult> PutTblCompanyByCompanyId(int companyId, TblCompany tblCompany)
        {
          // Check if the incoming contract's ID matches the specified contractId
          if (companyId != tblCompany.CompanyId)
          {
            return BadRequest();
          }

          // Find the existing contract in the database
          var existingCompany = await _context.TblCompanies.FindAsync(companyId);
          if (existingCompany == null)
          {
            return NotFound();
          }

          // Update the existing contract with the new values
          _context.Entry(existingCompany).CurrentValues.SetValues(tblCompany);


        existingCompany.UpdatedOn = DateTime.Now;

        try
          {
            await _context.SaveChangesAsync();
          }
          catch (DbUpdateConcurrencyException)
          {
            if (!TblCompanyExists(companyId))
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



      [HttpPost]
      public async Task<ActionResult<TblCompany>> PostTblCompany1(TblCompany tblCompany)
      {
        if (_context.TblCompanies == null)
        {
          return Problem("Entity set 'EcresMreContext.TblCompanies' is null.");
        }



        // Step 1: Check if the username already exists in the database
        var existingCompany = await _context.TblCompanies
                                            .FirstOrDefaultAsync(c => c.Username == tblCompany.Username);

        if (existingCompany != null)
        {
          // Return a conflict status if the username already exists
          return Conflict(new { message = "Username already exists. Please choose another username." });
        }


        // Step 2: Check if the license number already exists in the database
        var existingCompanyByLicense = await _context.TblCompanies
            .FirstOrDefaultAsync(c => c.LicenseNo == tblCompany.LicenseNo);

        if (existingCompanyByLicense != null)
        {
          // Return a conflict status if the license number already exists
          return Conflict(new { message = "License number already exists." });
        }


        // Step 1: Generate a random password
        var plainPassword = GenerateRandomPassword(12); // Generate a 12-character random password

        // Step 2: Hash the password before saving
        tblCompany.Password = HashPassword(plainPassword);

        // Step 3: Save the company details to the database
        _context.TblCompanies.Add(tblCompany);
        await _context.SaveChangesAsync();

        // Step 4: Prepare the email content
        var emailSubject = "Your Account Details for e-CRES";
        var emailBody = $@"
          <p>Dear {tblCompany.Username},</p>
          <p>Your account has been created successfully. Below are your account details:</p>
          <ul>
              <li><strong>Username:</strong> {tblCompany.Username}</li>
              <li><strong>Password:</strong> {plainPassword}</li>
          </ul>
          <p>Please log in to your account and change your password immediately for security purposes.</p>
          <p>You can access the e-CRES system by clicking the following link:</p>
          <p><a href='https://www5.lgm.gov.my/ecres/' target='_blank'>https://www5.lgm.gov.my/ecres/</a></p>
          <p>Thank you.</p>";

        // Step 5: Send the email using EmailServices
        var emailSent = await EmailServices.SendEmailAsync(tblCompany.Email1, emailSubject, emailBody);

        if (!emailSent)
        {
          return StatusCode(500, "Error sending email. Please try again later.");
        }

        // Step 6: Return the company details (excluding password hash)
        var result = new
        {
          tblCompany.CompanyId,
          tblCompany.Username,
          tblCompany.Email1
        };

        return CreatedAtAction("GetTblCompany", new { id = tblCompany.CompanyId }, result);
      }



      // Method to generate a random password
      private string GenerateRandomPassword(int length)
      {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
        var random = new Random();
        return new string(Enumerable.Repeat(validChars, length)
                                    .Select(s => s[random.Next(s.Length)]).ToArray());
      }

      // Method to hash the password using a secure algorithm (BCrypt)
      private string HashPassword(string password)
      {
        return BCrypt.Net.BCrypt.HashPassword(password);
      }



    // POST: api/TblCompanies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
        public async Task<ActionResult<TblCompany>> PostTblCompany(TblCompany tblCompany)
        {
          if (_context.TblCompanies == null)
          {
              return Problem("Entity set 'EcresMreContext.TblCompanies'  is null.");
          }
            _context.TblCompanies.Add(tblCompany);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblCompany", new { id = tblCompany.CompanyId }, tblCompany);
        }



    [HttpPost("SendPasswordEmail")]
    public async Task<IActionResult> SendPasswordEmail([FromBody] EmailPayload payload)
    {
      if (string.IsNullOrWhiteSpace(payload.Email1) || string.IsNullOrWhiteSpace(payload.Password))
      {
        return BadRequest("Email and password are required.");
      }

      try
      {
        // Compose the email
        var subject = "Your Account Details";
        var body = $@"
 
            Your account has been successfully created. Below are your login details:
            <br><br>
            <strong>Username:</strong> {payload.Username}<br>
            <strong>Password:</strong> {payload.Password}
            <br><br>
            Please keep this information secure.      
        ";

        // Send the email
        await EmailServices.SendEmailAsync(payload.Email1, subject, body);

        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500, "An error occurred while sending the email: " + ex.Message);
      }
    }

      // Payload class for email data
      public class EmailPayload
      {
        public string Email1 { get; set; }
        public string Username { get; set; }
       // public string CompanyName { get; set; }
        public string Password { get; set; }
        public string PlainPassword { get; set; }
      }



    [HttpPost]
    public async Task<ActionResult<TblCompany>> PostTblCompanyEcres(TblCompany tblCompany)
    {
      if (_context.TblCompanies == null)
      {
        return Problem("Entity set 'EcresMreContext.TblCompanies' is null.");
      }

      // Hash the password before saving
      if (!string.IsNullOrWhiteSpace(tblCompany.Password))
      {
        tblCompany.Password = BCrypt.Net.BCrypt.HashPassword(tblCompany.Password);
      }

      _context.TblCompanies.Add(tblCompany);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetTblCompany", new { id = tblCompany.CompanyId }, tblCompany);
    }



    // DELETE: api/TblCompanies/5
    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblCompany(int id)
        {
            if (_context.TblCompanies == null)
            {
                return NotFound();
            }
            var tblCompany = await _context.TblCompanies.FindAsync(id);
            if (tblCompany == null)
            {
                return NotFound();
            }

            _context.TblCompanies.Remove(tblCompany);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblCompanyExists(int id)
        {
            return (_context.TblCompanies?.Any(e => e.CompanyId == id)).GetValueOrDefault();
        }

    [HttpPut("{companyId}/status")]
    public async Task<IActionResult> UpdateCompanyStatus(int companyId, [FromBody] bool status)
    {
      var existingCompany = await _context.TblCompanies.FindAsync(companyId);
      if (existingCompany == null)
      {
        return NotFound();
      }

      existingCompany.Status = status;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TblCompanyExists(companyId))
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


    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPasswordEcres([FromBody] ForgotPasswordDTO forgotPasswordDTO)
    {
      // Check if the user exists by email or username
      var user = await _context.TblCompanies
          .FirstOrDefaultAsync(u => u.Username == forgotPasswordDTO.Username || u.Email1 == forgotPasswordDTO.Email1);

      if (user == null)
      {
        return BadRequest("User not found.");
      }

      // Generate a reset token and expiration date (valid for 1 hour)
      var resetToken = Guid.NewGuid().ToString();
      user.ResetToken = resetToken;
      user.ResetTokenExpiration = DateTime.UtcNow.AddHours(1);

      // Save the reset token and expiration in the database
      await _context.SaveChangesAsync();

      // Send the reset token via email
      //var resetLink = $"https://your-frontend-url.com/reset-password?token={resetToken}";
      var resetLink = $"http://localhost:4200/resetPassword/?token={resetToken}";
      var emailSent = await EmailServices.SendEmailAsync(user.Email, "Password Reset Request",
          $"Please click the following link to reset your password: {resetLink}");

      if (!emailSent)
      {
        return StatusCode(500, "Failed to send reset email.");
      }

      return Ok("Password reset link has been sent to your email.");
    }



    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
    {
      // Retrieve the user based on the username
      var user = await _context.TblCompanies
          .FirstOrDefaultAsync(u => u.Username == forgotPasswordDTO.Username);

      if (user == null)
      {
        return BadRequest(new { message = "User not found." });
      }

      // Parse the Email1 field into a list of individual email addresses
      var emailAddresses = user.Email1?.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(email => email.Trim())
                                        .ToList();

      if (emailAddresses == null || !emailAddresses.Any())
      {
        return BadRequest(new { message = "No valid email found for the user." });
      }

      // Check if the entered email matches any of the parsed email addresses
      var matchedEmail = emailAddresses.FirstOrDefault(email => email.Equals(forgotPasswordDTO.Email1, StringComparison.OrdinalIgnoreCase));

      if (matchedEmail == null)
      {
        return BadRequest(new { message = "The entered email does not match any associated email for the user." });
      }

      // Generate a reset token and expiration date (valid for 1 hour)
      var resetToken = Guid.NewGuid().ToString();
      user.ResetToken = resetToken;
      user.ResetTokenExpiration = DateTime.UtcNow.AddHours(1);

      // Save the reset token and expiration in the database
      await _context.SaveChangesAsync();

      // Send the reset token to the matched email
      var resetLink = $"https://www5.lgm.gov.my/ecres/resetPassword/?token={resetToken}";
      var emailSent = await EmailServices.SendEmailAsync(matchedEmail, "Password Reset Request",
          $"Please click the following link to reset your password: {resetLink}");

      if (!emailSent)
      {
        return StatusCode(500, new { message = "Failed to send reset email." });
      }

      return Ok(new { message = "Password reset link has been sent to your email." });
    }



    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
    {
      // Find the user by the reset token
      var user = await _context.TblCompanies
          .FirstOrDefaultAsync(u => u.ResetToken == resetPasswordDTO.Token && u.ResetTokenExpiration > DateTime.UtcNow);

      if (user == null)
      {

        return BadRequest(new { message = "Invalid or expired reset token." });
      }

      // Hash the new password before saving
      user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDTO.NewPassword);

      // Clear the reset token and expiration
      user.ResetToken = null;
      user.ResetTokenExpiration = null;

      await _context.SaveChangesAsync();

      return Ok(new { message = "Password has been reset successfully." });
    }

  }
}
