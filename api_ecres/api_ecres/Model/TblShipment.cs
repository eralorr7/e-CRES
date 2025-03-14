using System;
using System.Collections.Generic;

namespace api_ecres.Model;

public partial class TblShipment
{
    public int ShipmentId { get; set; }

    public string? ShipmentType { get; set; }

    public bool? Status { get; set; }
}
