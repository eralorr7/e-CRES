using System;
using System.Collections.Generic;

namespace api_ecres.Model;

public partial class TblRubberType
{
    public int RubberId { get; set; }

    public string? RubberType { get; set; }

    public bool? Status { get; set; }
}
