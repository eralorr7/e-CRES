using System;
using System.Collections.Generic;

namespace api_ecres.Model;

public partial class TblShipmentTerm
{
    public int ShipmentTermId { get; set; }

    public string? ShipmentTerm { get; set; }

    public bool? Status { get; set; }
}
