using System;
using System.Collections.Generic;

namespace api_ecres.Model;

public partial class TblContract1
{
    public int ContractId { get; set; }

    public int? CompanyId { get; set; }

    public string? ContractType { get; set; }

    public string? ContractNo { get; set; }

    public string? ContractDate { get; set; }

    public int? ShipmentId { get; set; }

    public string? Month1 { get; set; }

    public string? Month2 { get; set; }

    public string? BuyerSeller { get; set; }

    public int? RubberId { get; set; }

    public string? RemarksCentrifugedLatex { get; set; }

    public decimal? Quantity { get; set; }

    public string? Currency { get; set; }

    public decimal? Price { get; set; }

    public decimal? PriceEquivalent { get; set; }

    public int? ShipmentTermId { get; set; }

    public string? OtherTerm { get; set; }

    public string? PlaceFactoryPort { get; set; }

    public string? Destination { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? DeletedStatus { get; set; }

    public bool? IsDraft { get; set; }

    public decimal? QuantityActual { get; set; }

    public string? Unit { get; set; }

    public string? Trade { get; set; }

    public int? StatusId { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? ResubmitDate { get; set; }
}
