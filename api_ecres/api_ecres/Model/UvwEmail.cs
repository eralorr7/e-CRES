using System;
using System.Collections.Generic;

namespace api_ecres.Model;

public partial class UvwEmail
{
    public string? Email { get; set; }

    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public bool Status { get; set; }
}
