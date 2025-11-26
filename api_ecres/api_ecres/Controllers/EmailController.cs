using api_ecres.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Net;
using System.Net.Mail;

namespace api_ecres.Controllers
{


  [Route("api/[controller]/[action]")]
  [ApiController]
  public class EmailController : ControllerBase
  {
    private readonly EcresMreContext _context;
    private readonly IConfiguration _config;

    public EmailController(EcresMreContext context, IConfiguration config)
    {
      _context = context;
      _config = config;
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


    [HttpPost("sendEmailsToCompanies")]
    public async Task<IActionResult> SendEmailsToCompanies()
    {
      // Fetch all emails from tblCompanies where status is true, regardless of domain
      var companies = await _context.TblCompanies
          .Where(x => x.Status == true && !string.IsNullOrEmpty(x.Email1)) // Ensure Email1 is not null or empty
          .Select(x => x.Email1)
          .ToListAsync();

      var subject = "KEPERLUAN PEMEGANG LESEN UNTUK MELAPORKAN DATA URUSNIAGA HARIAN BAGI KONTRAK JUAL BELI GETAH KEPADA LEMBAGA GETAH MALAYSIA (LGM)";
      var message = $@"
        Salam Sejahtera,
        <br><br>
        YBhg. Datuk/Dato’/Tuan/Puan,
        <br><br>
        Selamat datang ke sistem Electronic Contract Reporting System (e-CRES).
        <br><br>
        Untuk melaporkan data urusniaga harian bagi kontrak jual beli getah bagi gred SMR 10, SMR 20, China Mixture Rubber dan Lateks Pukal, sila layari sistem e-CRES dengan klik link berikut:
        <br><br>
        https://www5.lgm.gov.my/ecres/
        <br><br>
        Untuk mengakses sistem e-CRES, sila masukkan ID Pengguna dan Kata Laluan yang telah diberikan kepada syarikat masing-masing sebelum ini.
        <br><br>
        Pemegang lesen dikehendaki melaporkan data yang diperlukan oleh LGM berdasarkan kontrak dagangan sebenar yang telah dipersetujui di antara kedua-dua pihak pembeli dan penjual.
        <br><br>
        NOTA: MOHON ABAIKAN NOTIFIKASI INI JIKA TIADA SEBARANG URUSNIAGA PADA HARI INI
        <br><br>
        Sebarang pertanyaan lanjut, sila hubungi Malaysian Rubber Exchange:
        <br><br>
        <strong>Malaysian Rubber Exchange (MRE) </strong>
        <br>
        Economics, Licensing & Enforcement Division
        <br>
        Malaysian Rubber Board
        <br>
        T: 03-92062092
        <br>
        F: 03-21616586
        <br>
        E : mre1@lgm.gov.my 
        <br><br>
        Ms Chuah Jia Min – T: 03-92062093 / E: jmchuah@lgm.gov.my
        <br>
        En Najmuddin – T: 03-92062172/2116 / E: najmuddin@lgm.gov.my
        <br><br>
        Sekian, terima kasih.
        <br><br><br>

  <i> <strong> REF: REQUIREMENT FOR LICENSEES TO REPORT DAILY TRANSACTION DATA FOR PURCHASE AND SALES OF RUBBER CONTRACTS TO MALAYSIAN RUBBER BOARD(MRB) </strong>
          <br><br>
           Welcome to Electronic Contract Reporting System(e-CRES).
          <br><br>
           To report daily transaction data for trading contracts of SMR 10, SMR 20, China Mixture Rubber and Centrifuged Latex contracts, please visit the e - CRES system by clicking the following link: 
           <br><br>
           https://www5.lgm.gov.my/ecres/
          <br><br>
           In order to access the e - CRES system, please enter the User ID and Password which have been provided previously to their respective companies.
          <br><br>
           The licensee is required to report the data to LGM based on the actual trading contract agreed between buyer and seller.
          <br><br>
           NOTE: PLEASE IGNORE THIS NOTIFICATION IF THERE IS NO TRADE ON TODAY.
          <br><br>
           For further clarification, kindly contact the Malaysian Rubber Exchange:
          <br><br>
           <strong>Malaysian Rubber Exchange (MRE) </strong>
           <br>
           Economics, Licensing & Enforcement Division
           <br>
           Malaysian Rubber Board
           <br>
           T: 03-92062092
           <br>
           F: 03-21616586
           <br>
           E : mre1@lgm.gov.my 
           <br><br>
           Ms Chuah Jia Min – T: 03-92062093 / E: jmchuah@lgm.gov.my
           <br>
           En Najmuddin – T: 03-92062172/2116 / E: najmuddin@lgm.gov.my
           <br><br>
           Thank You.
           <br><br>
           Malaysian Rubber Exchange
           <br>
           Lembaga Getah Malaysia
           <br>
           T: 03-92062092
           <br>
           F: 03-21616586
           <br>
           E : mre1@lgm.gov.my
           <br> </i>";

      // Set to store unique emails
      var uniqueEmails = new HashSet<string>();

      // List to store failed email addresses
      var failedEmails = new List<string>();

      // Loop through each email from the database, split by ';' or ','
      foreach (var emailList in companies)
      {
        // Split the emails by semicolon or comma
        var emails = emailList.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

        // Add emails to HashSet to ensure uniqueness
        foreach (var email in emails)
        {
          // Trim any leading/trailing spaces and add to the HashSet
          uniqueEmails.Add(email.Trim());
        }
      }

      // Loop through each unique email and send the message
      foreach (var email in uniqueEmails)
      {
        var emailSent = await EmailServices.SendEmailAsync(email, subject, message);
        if (!emailSent)
        {
          failedEmails.Add(email); // Add to failed emails for reporting
        }
      }

      if (failedEmails.Count > 0)
      {
        // Return error status with details of failed emails
        return StatusCode((int)HttpStatusCode.InternalServerError, new
        {
          message = $"Error sending emails to the following addresses: {string.Join(", ", failedEmails)}"
        });
      }

      // Return success status with success message
      return Ok(new { message = "Emails sent successfully to all addresses." });
    }

  }
}
