using System;
using System.Collections.Generic;

namespace api_ecres.Model;

public partial class State
{
    public int Id { get; set; }

    public string StateCode { get; set; } = null!;

    public string? StateName { get; set; }

    public string? StateCodeSmp { get; set; }
}
