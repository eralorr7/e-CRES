using System;
using System.Collections.Generic;

namespace api_ecres.Model;

public partial class TblUploadUserManual
{
    public int Id { get; set; }

    public string? UserManual { get; set; }

    public bool? Status { get; set; }
}
