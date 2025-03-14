using System;
using System.Collections.Generic;

namespace api_ecres.Model;

public partial class UvwExcelMre
{
    public int ContractId { get; set; }

    public string? ContractType { get; set; }

    public string? CompanyName { get; set; }

    public string? ContractNo { get; set; }

    public DateTime? ContractDate { get; set; }

    public string? ShipmentType { get; set; }

    public string? Month1 { get; set; }

    public string? Month2 { get; set; }

    public string? BuyerSeller { get; set; }

    public string? RubberType { get; set; }

    public string? RemarksCentrifugedLatex { get; set; }

    public decimal? Quantity { get; set; }

    public string? Currency { get; set; }

    public decimal? Price { get; set; }

    public decimal? PriceEquivalent { get; set; }

    public string? ShipmentTerm { get; set; }

    public string? OtherTerm { get; set; }

    public string? PlaceFactoryPort { get; set; }

    public string? Destination { get; set; }

    public string? CompanyId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Type { get; set; }
}
