/*using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace api_ecres
{
  public class EmailServices
  {
    public static async Task<bool> SendEmailAsync(string recipient, string subject, string message)
    {
      var emailMessage = new MimeMessage();
      var builder = new BodyBuilder();

      emailMessage.From.Add(new MailboxAddress("ecres", "admin_ecres@lgm.gov.my"));
      emailMessage.To.Add(new MailboxAddress("", recipient));
      emailMessage.Subject = subject;
      builder.HtmlBody = message;
      emailMessage.Body = builder.ToMessageBody();

      try
      {
        using (var client = new SmtpClient())
        {
          // Connect to the SMTP server with required configurations
          client.LocalDomain = "lgm.gov.my"; // Optional for your SMTP server
          await client.ConnectAsync("10.4.137.18", 25, SecureSocketOptions.None).ConfigureAwait(false);
          await client.SendAsync(emailMessage).ConfigureAwait(false);
          await client.DisconnectAsync(true).ConfigureAwait(false);
        }
        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Failed to send email via internal SMTP: {ex.Message}");

        // Optional: Try sending via an external SMTP server if needed
        try
        {
          using (var client = new SmtpClient())
          {
            // Example external SMTP (Gmail)
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls).ConfigureAwait(false);
            await client.AuthenticateAsync("nursolehahmazlan04@gmail.com", "uzyv rrqb ddlh wpsz").ConfigureAwait(false);
            await client.SendAsync(emailMessage).ConfigureAwait(false);
            await client.DisconnectAsync(true).ConfigureAwait(false);
          }
          return true;
        }
        catch (Exception extEx)
        {
          Console.WriteLine($"Failed to send email via external SMTP: {extEx.Message}");
          return false;
        }
      }
    }


  }
}*/





using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace api_ecres
{
  public class EmailServices
  {
    public static async Task<bool> SendEmailAsync(string recipient, string subject, string message)
    {
      try
      {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("ecres", "admin_ecres@lgm.gov.my"));
        emailMessage.To.Add(new MailboxAddress("", recipient));
        emailMessage.Subject = subject;

        //var builder = new BodyBuilder();
        var builder = new BodyBuilder
        {
          HtmlBody = $@"
                      
                        <p>{message}</p>"
        };

        emailMessage.Body = builder.ToMessageBody();

        using (var client = new SmtpClient())
        {
          // Use Port 25 (No Authentication, No Encryption)
          await client.ConnectAsync("10.4.137.105", 25, SecureSocketOptions.None);

          // Send email
          await client.SendAsync(emailMessage);
          await client.DisconnectAsync(true);
        }

        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Email sending failed: {ex.Message}");
        return false;
      }
    }
  }
}



