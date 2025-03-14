namespace api_ecres.Model
{
  public class EmailRequest
  {
    public string To { get; set; }  // Recipient's email
    public string Subject { get; set; }  // Email subject
    public string Html { get; set; }  // Email body (HTML content)
  }
}
