using System;
using System.Collections.Generic;

namespace api_ecres.Model;

public partial class TblPriceEquivalent
{
    public int? Id { get; set; }

    public decimal? ForexRate { get; set; }

    public decimal? PriceEquivalent { get; set; }

    public int? RubberId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ShipmentTermId { get; set; }

    public bool? Status { get; set; }
}
