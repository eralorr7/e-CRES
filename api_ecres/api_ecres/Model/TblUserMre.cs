using System;
using System.Collections.Generic;

namespace api_ecres.Model;

public partial class TblUserMre
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }
}
