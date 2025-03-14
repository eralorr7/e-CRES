using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_ecres.Model;
using System.ComponentModel.Design;

namespace api_ecres.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class TblContractsController : ControllerBase
  {
    private readonly EcresMreContext _context;

    public TblContractsController(EcresMreContext context)
    {
      _context = context;
    }

    // GET: api/TblContracts
    /* [HttpGet]
     public async Task<ActionResult<IEnumerable<TblContract>>> GetTblContracts()
     {
       if (_context.TblContracts == null)
       {
           return NotFound();
       }
         return await _context.TblContracts.ToListAsync();
     }*/




  [HttpGet]
public async Task<IActionResult> GetTblContracts()
{
      var contracts = await _context.TblContracts.ToListAsync(); // ✅ Fetch data first
   /*   var contracts = await _context.TblContracts
           .OrderByDescending(c => c.CreatedDate) // Optional: Order by latest
           .Take(10)
           .ToListAsync();*/
      var rubberTypes = await _context.TblRubberTypes.ToListAsync(); // ✅ Fetch rubber types once

    var contractList = contracts.Select(c => new
    {
        contractId = c.ContractId,
        contractType = c.ContractType,
        companyId = c.CompanyId,
        companyName = _context.TblCompanies
            .Where(y => y.CompanyId == c.CompanyId)
            .Select(y => y.CompanyName)
            .FirstOrDefault(),
        contractNo = c.ContractNo,
        contractDate = c.ContractDate,
        shipmentId = c.ShipmentId,
        shipmentType = _context.TblShipments
            .Where(y => y.ShipmentId == c.ShipmentId)
            .Select(y => y.ShipmentType)
            .FirstOrDefault(),
        month1 = c.Month1,
        month2 = c.Month2,
        buyerSeller = c.BuyerSeller,
        rubberId = c.RubberId,

        // ✅ Process RubberId safely (Convert string to a list of integers)
        rubberType = !string.IsNullOrEmpty(c.RubberId)
            ? rubberTypes
                .Where(y => c.RubberId.Split(',')
                    .Select(id => int.TryParse(id, out int num) ? num : (int?)null)  // Convert string to int safely
                    .Where(num => num.HasValue) // Remove null values
                    .Contains(y.RubberId)) // Match rubber IDs
                .Select(y => y.RubberType)
                .ToList()
            : new List<string>(), // If RubberId is empty, return an empty list

        remarksCentrifugedLatex = c.RemarksCentrifugedLatex,
        quantity = c.Quantity,
        currency = c.Currency,
        trade = c.Trade,
        price = c.Price,
        priceEquivalent = c.PriceEquivalent,
      shipmentTermId = c.ShipmentTermId,
      shipmentTerm = _context.TblShipmentTerms
            .Where(y => y.ShipmentTermId == c.ShipmentTermId)
            .Select(y => y.ShipmentTerm) // Get the actual Shipment Term name
            .FirstOrDefault(),
      otherTerm = c.OtherTerm,
        placeFactoryPort = c.PlaceFactoryPort,
        destination = c.Destination,
        createdDate = c.CreatedDate,
        updatedDate = c.UpdatedDate,
        resubmitDate = c.ResubmitDate,

    }).ToList();

    return Ok(contractList);
}


    

    [HttpGet]
    public async Task<IActionResult> GetTblContractByStatus(int statusId)
    {
      if (_context.TblContracts == null)
      {
        return NotFound("Contracts table not found.");
      }

      // ✅ Fetch all rubber types once
      var rubberTypes = await _context.TblRubberTypes.ToListAsync();

      var contracts = await _context.TblContracts
          .Where(c => c.StatusId == statusId)
          .ToListAsync(); // ✅ Fetch contracts first

      var result = contracts.Select(c =>
      {
        // ✅ Convert RubberId (string) to a list of integers
        var rubberIdList = c.RubberId?.Split(',')
            .Select(id => int.TryParse(id, out int num) ? num : (int?)null) // Convert to int safely
            .Where(num => num.HasValue) // Remove null values
            .Select(num => num.Value) // Convert nullable int to int
            .ToList() ?? new List<int>();

        // ✅ Get rubber type names
        var rubberTypeList = rubberTypes
            .Where(y => rubberIdList.Contains(y.RubberId))
            .Select(y => y.RubberType)
            .ToList();

        return new
        {
          contractId = c.ContractId,
          contractType = c.ContractType,
          companyId = c.CompanyId,
          companyName = _context.TblCompanies
                .Where(y => y.CompanyId == c.CompanyId)
                .Select(y => y.CompanyName)
                .FirstOrDefault(),
          contractNo = c.ContractNo,
          contractDate = c.ContractDate,
          shipmentId = c.ShipmentId,
          shipmentType = _context.TblShipments
                .Where(y => y.ShipmentId == c.ShipmentId)
                .Select(y => y.ShipmentType)
                .FirstOrDefault(),
          month1 = c.Month1,
          month2 = c.Month2,
          buyerSeller = c.BuyerSeller,
          rubberId = c.RubberId,
          rubberTypes = rubberTypeList, // ✅ Corrected rubberType
          remarksCentrifugedLatex = c.RemarksCentrifugedLatex,
          quantity = c.Quantity,
          currency = c.Currency,
          price = c.Price,
          priceEquivalent = c.PriceEquivalent,
          shipmentTermId = c.ShipmentTermId,
          shipmentTerm = _context.TblShipmentTerms
                .Where(y => y.ShipmentTermId == c.ShipmentTermId)
                .Select(y => y.ShipmentTerm)
                .FirstOrDefault(),
          otherTerm = c.OtherTerm,
          placeFactoryPort = c.PlaceFactoryPort,
          destination = c.Destination,
          createdDate = c.CreatedDate,
          updatedDate = c.UpdatedDate,
          resubmitDate = c.ResubmitDate,
        };
      }).ToList();

      return Ok(result);
    }




    [HttpGet]
    public async Task<IActionResult> GetTblContractByStatus1(int statusId)
    {
      var currentDate = DateTime.UtcNow;

      // ✅ Fetch contracts first
      var contracts = await _context.TblContracts
          .Where(c => c.StatusId == 2 &&
                      c.CreatedDate.HasValue &&
                      c.CreatedDate.Value.Month == currentDate.Month &&  // Ensure it's the same month
                      c.CreatedDate.Value.AddDays(14) >= currentDate) // Within 14 days
          .ToListAsync();

      if (!contracts.Any())
      {
        return NotFound($"No contracts found with status ID: {statusId}");
      }

      // ✅ Fetch all related data
      var companies = await _context.TblCompanies.ToListAsync();
      var shipments = await _context.TblShipments.ToListAsync();
      var shipmentTerms = await _context.TblShipmentTerms.ToListAsync();
      var rubberTypes = await _context.TblRubberTypes.ToListAsync();

      // ✅ Convert to response model
      var result = contracts.Select(c =>
      {
        var rubberIdList = c.RubberId?.Split(',')
            .Select(id => int.TryParse(id, out int num) ? num : (int?)null)
            .Where(num => num.HasValue)
            .Select(num => num.Value)
            .ToList() ?? new List<int>();

        var rubberTypeList = rubberTypes
            .Where(y => rubberIdList.Contains(y.RubberId))
            .Select(y => y.RubberType)
            .ToList();

        return new
        {
          contractId = c.ContractId,
          contractType = c.ContractType,
          companyId = c.CompanyId,
          companyName = companies.FirstOrDefault(y => y.CompanyId == c.CompanyId)?.CompanyName,
          contractNo = c.ContractNo,
          contractDate = c.ContractDate,
          shipmentId = c.ShipmentId,
          shipmentType = shipments.FirstOrDefault(y => y.ShipmentId == c.ShipmentId)?.ShipmentType,
          month1 = c.Month1,
          month2 = c.Month2,
          buyerSeller = c.BuyerSeller,
          rubberId = c.RubberId,
          rubberTypes = rubberTypeList, // ✅ Fixed rubber type display
          remarksCentrifugedLatex = c.RemarksCentrifugedLatex,
          quantity = c.Quantity,
          currency = c.Currency,
          price = c.Price,
          priceEquivalent = c.PriceEquivalent,
          shipmentTermId = c.ShipmentTermId,
          shipmentTerm = shipmentTerms.FirstOrDefault(y => y.ShipmentTermId == c.ShipmentTermId)?.ShipmentTerm,
          otherTerm = c.OtherTerm,
          placeFactoryPort = c.PlaceFactoryPort,
          destination = c.Destination,
          createdDate = c.CreatedDate,
          updatedDate = c.UpdatedDate,
          resubmitDate = c.ResubmitDate,
        };
      }).ToList();

      return Ok(result);
    }



    [HttpGet]
    public async Task<IActionResult> SearchTblContractByStatus(int statusId, string contractNo, string? contractType = null, string? companyName = null)
    {
      // Check if contractNo is provided, if not return a BadRequest
      if (string.IsNullOrEmpty(contractNo))
      {
        return BadRequest("ContractNo is required.");
      }

      // Set current date
      var currentDate = DateTime.UtcNow;

      // Start building the query with status and date filters
      var contractQuery = _context.TblContracts
          .Where(c => c.StatusId == 2 &&
                      c.CreatedDate.HasValue &&
                      c.CreatedDate.Value.Month == currentDate.Month &&
                      (
                          (c.CreatedDate.Value.AddDays(14) >= currentDate && c.CreatedDate.Value.Month == currentDate.Month) ||
                          (currentDate >= c.CreatedDate.Value && currentDate <= c.CreatedDate.Value.AddDays(14) && c.CreatedDate.Value.Month == currentDate.Month)
                      ));

      // Apply contractNo filter (Required)
      contractQuery = contractQuery.Where(c => c.ContractNo.Contains(contractNo));

      // Apply contractType filter (Optional)
      if (!string.IsNullOrEmpty(contractType))
      {
        contractQuery = contractQuery.Where(c => c.ContractType.Contains(contractType));
      }

      // Apply companyName filter (Optional)
      if (!string.IsNullOrEmpty(companyName))
      {
        contractQuery = contractQuery.Where(c => _context.TblCompanies
                .Where(y => y.CompanyId == c.CompanyId)
                .Select(y => y.CompanyName)
                .FirstOrDefault().Contains(companyName));
      }

      var contracts = await contractQuery
          .Select(c => new
          {
            contractId = c.ContractId,
            contractType = c.ContractType,
            companyId = c.CompanyId,
            companyName = _context.TblCompanies
                      .Where(y => y.CompanyId == c.CompanyId)
                      .Select(y => y.CompanyName)
                      .FirstOrDefault(),
            contractNo = c.ContractNo,
            contractDate = c.ContractDate,
            shipmentId = c.ShipmentId,
            shipmentType = _context.TblShipments
                      .Where(y => y.ShipmentId == c.ShipmentId)
                      .Select(y => y.ShipmentType)
                      .FirstOrDefault(),
            month1 = c.Month1,
            month2 = c.Month2,
            buyerSeller = c.BuyerSeller,
            rubberId = c.RubberId,
           /* rubberType = _context.TblRubberTypes
                      .Where(y => y.RubberId == c.RubberId)
                      .Select(y => y.RubberType)
                      .FirstOrDefault(),*/
            remarksCentrifugedLatex = c.RemarksCentrifugedLatex,
            quantity = c.Quantity,
            currency = c.Currency,
            price = c.Price,
            priceEquivalent = c.PriceEquivalent,
            shipmentTermId = c.ShipmentTermId,
            shipmentTerm = _context.TblShipmentTerms
                      .Where(y => y.ShipmentTermId == c.ShipmentTermId)
                      .Select(y => y.ShipmentTerm)
                      .FirstOrDefault(),
            otherTerm = c.OtherTerm,
            placeFactoryPort = c.PlaceFactoryPort,
            destination = c.Destination,
            createdDate = c.CreatedDate,
            updatedDate = c.UpdatedDate,
            resubmitDate = c.ResubmitDate,
          })
          .ToListAsync();

      return Ok(contracts);
    }

    


    [HttpGet("{contractId}")]
      public async Task<IActionResult> GetTblContractByContractId2(int contractId)
      {
        if (_context.TblContracts == null)
        {
          return NotFound();
        }

        var tblContract = await _context.TblContracts
            .Where(c => c.ContractId == contractId)
            .Select(c => new
            {
              contractId = c.ContractId,
              contractType = c.ContractType,
              companyId = c.CompanyId,
              companyName = _context.TblCompanies.Where(y => y.CompanyId == c.CompanyId).Select(y => y.CompanyName).FirstOrDefault(),
              contractNo = c.ContractNo,
              contractDate = c.ContractDate,
              shipmentId = c.ShipmentId,
              shipmentType = _context.TblShipments.Where(y => y.ShipmentId == c.ShipmentId).Select(y => y.ShipmentType).FirstOrDefault(),
              month1 = c.Month1,
              month2 = c.Month2,
              buyerSeller = c.BuyerSeller,
              rubberId = c.RubberId,
       
              //rubberType = _context.TblRubberTypes.Where(y => y.RubberId == c.RubberId).Select(y => y.RubberType).FirstOrDefault(),
              remarksCentrifugedLatex = c.RemarksCentrifugedLatex,
              quantity = c.Quantity,
              unit = c.Unit,
              quantityActual = c.QuantityActual,
              currency = c.Currency,
              trade = c.Trade,
              price = c.Price,
              priceEquivalent = c.PriceEquivalent,
              shipmentTermId = c.ShipmentTermId,
              shipmentTerm = _context.TblShipmentTerms.Where(y => y.ShipmentTermId == c.ShipmentTermId).Select(y => y.ShipmentTerm).FirstOrDefault(),
              otherTerm = c.OtherTerm,
              placeFactoryPort = c.PlaceFactoryPort,
              destination = c.Destination,
              createdDate = c.CreatedDate,
              updatedDate = c.UpdatedDate,
              resubmitDate = c.ResubmitDate,
            })
            .FirstOrDefaultAsync(); // Use FirstOrDefaultAsync to get a single contract

        if (tblContract == null)
        {
          return NotFound("No contract found with the specified ID.");
        }

        return Ok(tblContract);
      }


    [HttpGet("{contractId}")]
    public async Task<IActionResult> GetTblContractByContractId(int contractId)
    {
      if (_context.TblContracts == null)
      {
        return NotFound();
      }

      var tblContract = await _context.TblContracts
          .Where(c => c.ContractId == contractId)
          .Select(c => new
          {
            contractId = c.ContractId,
            contractType = c.ContractType,
            companyId = c.CompanyId,
            companyName = _context.TblCompanies
                  .Where(y => y.CompanyId == c.CompanyId)
                  .Select(y => y.CompanyName)
                  .FirstOrDefault(),
            contractNo = c.ContractNo,
            contractDate = c.ContractDate,
            shipmentId = c.ShipmentId,
            shipmentType = _context.TblShipments
                  .Where(y => y.ShipmentId == c.ShipmentId)
                  .Select(y => y.ShipmentType)
                  .FirstOrDefault(),
            month1 = c.Month1,
            month2 = c.Month2,
            buyerSeller = c.BuyerSeller,
            rubberId = c.RubberId, // Keep rubberId as original
            remarksCentrifugedLatex = c.RemarksCentrifugedLatex,
            quantity = c.Quantity,
            unit = c.Unit,
            quantityActual = c.QuantityActual,
            currency = c.Currency,
            trade = c.Trade,
            price = c.Price,
            priceEquivalent = c.PriceEquivalent,
            shipmentTermId = c.ShipmentTermId,
            shipmentTerm = _context.TblShipmentTerms
                  .Where(y => y.ShipmentTermId == c.ShipmentTermId)
                  .Select(y => y.ShipmentTerm)
                  .FirstOrDefault(),
            otherTerm = c.OtherTerm,
            placeFactoryPort = c.PlaceFactoryPort,
            destination = c.Destination,
            createdDate = c.CreatedDate,
            updatedDate = c.UpdatedDate,
            resubmitDate = c.ResubmitDate
          })
          .FirstOrDefaultAsync();

      if (tblContract == null)
      {
        return NotFound("No contract found with the specified ID.");
      }

      // Process rubberId conversion and fetch corresponding rubberType
      List<string> rubberTypes = new List<string>();
      if (!string.IsNullOrEmpty(tblContract.rubberId))
      {
        var rubberIds = tblContract.rubberId.Split(',')
            .Select(id => id.Trim()) // Remove spaces
            .Where(id => int.TryParse(id, out _)) // Ensure valid numbers
            .Select(int.Parse) // Convert to int
            .ToList();

        rubberTypes = await _context.TblRubberTypes
            .Where(rt => rubberIds.Contains(rt.RubberId))
            .Select(rt => rt.RubberType)
            .ToListAsync();
      }

      // Ensure updatedDate is not null and calculate trading days only if valid
      var tradingDays = tblContract.updatedDate.HasValue ? CalculateTradingDays(tblContract.updatedDate.Value) : 0;

      // Set the flag to determine if the Submit button should be enabled
      var isSubmitEnabled = tradingDays <= 3;

      // Return the properties directly along with the submit button status
      return Ok(new
      {
        tblContract.contractId,
        tblContract.contractType,
        tblContract.companyId,
        tblContract.companyName,
        tblContract.contractNo,
        tblContract.contractDate,
        tblContract.shipmentId,
        tblContract.shipmentType,
        tblContract.month1,
        tblContract.month2,
        tblContract.buyerSeller,
        tblContract.rubberId,
        rubberType = string.Join(", ", rubberTypes), // Combine multiple rubberTypes into a single string
        tblContract.remarksCentrifugedLatex,
        tblContract.quantity,
        tblContract.unit,
        tblContract.quantityActual,
        tblContract.currency,
        tblContract.trade,
        tblContract.price,
        tblContract.priceEquivalent,
        tblContract.shipmentTermId,
        tblContract.shipmentTerm,
        tblContract.otherTerm,
        tblContract.placeFactoryPort,
        tblContract.destination,
        tblContract.createdDate,
        tblContract.updatedDate,
        tblContract.resubmitDate,
        isSubmitEnabled
      });
    }

    private int CalculateTradingDays(DateTime updatedDate)
    {
      var tradingDays = 0;
      var currentDate = DateTime.Now.Date;

      // Loop through each day from updatedDate to currentDate
      for (var date = updatedDate.Date; date <= currentDate; date = date.AddDays(1))
      {
        // Check if the day is a weekday (Monday to Friday)
        if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
        {
          tradingDays++;
        }
      }

      return tradingDays;
    }




    [HttpGet("draft/{companyId}")]
    public async Task<IActionResult> GetDraftTblContractByCompanyId(int companyId)
    {
      // Modify the query to filter by statusId == 1 and remove isDraft condition
      var tblContracts = await _context.TblContracts
          .Where(x => x.CompanyId == companyId && x.StatusId == 1) // Filter by CompanyId and statusId == 1
          .OrderByDescending(x => x.ContractId)                    // Order by ContractId
          .Select(x => new
          {
            companyId = x.CompanyId,
            companyName = _context.TblCompanies
                  .Where(y => y.CompanyId == x.CompanyId)
                  .Select(y => y.CompanyName)
                  .FirstOrDefault(),
            contractType = x.ContractType,
            contractNo = x.ContractNo,
            contractDate = x.ContractDate,
            contractId = x.ContractId // Example of another property
          })
          .ToListAsync(); // Ensure the query is executed and results are materialized

      if (tblContracts == null || !tblContracts.Any()) // Check if no contracts were found
      {
        return NotFound("Batch not found");
      }

      return Ok(tblContracts);
    }


    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetTblContractByCompanyId(int companyId)
    {
      // ✅ Pre-fetch related data to reduce DB calls
      var companies = await _context.TblCompanies.ToListAsync();
      var shipments = await _context.TblShipments.ToListAsync();
      var shipmentTerms = await _context.TblShipmentTerms.ToListAsync();
      var rubberTypes = await _context.TblRubberTypes.ToListAsync();

      // ✅ Fetch contracts first
      var tblContracts = await _context.TblContracts
          .Where(x => x.CompanyId == companyId && (x.StatusId == 2 || x.StatusId == null))
          .OrderByDescending(x => x.ContractId)
          .ToListAsync(); // ✅ Execute before transformations

      if (!tblContracts.Any())
      {
        return NotFound("No contracts found for the specified company.");
      }

      // ✅ Convert to response model
      var result = tblContracts.Select(x =>
      {
        var rubberIdList = x.RubberId?.Split(',')
            .Select(id => int.TryParse(id, out int num) ? num : (int?)null)
            .Where(num => num.HasValue)
            .Select(num => num.Value)
            .ToList() ?? new List<int>();

        var rubberTypeList = rubberTypes
            .Where(y => rubberIdList.Contains(y.RubberId))
            .Select(y => y.RubberType)
            .ToList();

        return new
        {
          contractId = x.ContractId,
          contractType = x.ContractType,
          companyId = x.CompanyId,
          companyName = companies.FirstOrDefault(y => y.CompanyId == x.CompanyId)?.CompanyName,
          contractNo = x.ContractNo,
          contractDate = x.ContractDate,
          shipmentId = x.ShipmentId,
          shipmentType = shipments.FirstOrDefault(y => y.ShipmentId == x.ShipmentId)?.ShipmentType,
          month1 = x.Month1,
          month2 = x.Month2,
          buyerSeller = x.BuyerSeller,
          rubberId = x.RubberId,
          rubberTypes = rubberTypeList, // ✅ Fixed rubber type display
          remarksCentrifugedLatex = x.RemarksCentrifugedLatex,
          quantity = x.Quantity,
          currency = x.Currency,
          price = x.Price,
          priceEquivalent = x.PriceEquivalent,
          shipmentTermId = x.ShipmentTermId,
          shipmentTerm = shipmentTerms.FirstOrDefault(y => y.ShipmentTermId == x.ShipmentTermId)?.ShipmentTerm,
          otherTerm = x.OtherTerm,
          placeFactoryPort = x.PlaceFactoryPort,
          destination = x.Destination,
          createdDate = x.CreatedDate,
          updatedDate = x.UpdatedDate,
          resubmitDate = x.ResubmitDate,
          trade = x.Trade
        };
      }).ToList();

      return Ok(result);
    }



    [HttpPut("{contractId}")]
    public async Task<IActionResult> PutTblContractByContractId1(int contractId, TblContract tblContract)
    {
      // Check if the incoming contract's ID matches the specified contractId
      if (contractId != tblContract.ContractId)
      {
        return BadRequest();
      }

      // Find the existing contract in the database
      var existingContract = await _context.TblContracts.FindAsync(contractId);
      if (existingContract == null)
      {
        return NotFound();
      }

      // Check if the updated_date is not null, if so, set resubmit_date to the current date
      if (existingContract.UpdatedDate != null)
      {
        tblContract.ResubmitDate = DateTime.Now; // Set resubmit_date if updated_date is not null
      }
      else
      {
        tblContract.ResubmitDate = null; // Do not set resubmit_date if updated_date is null
      }

      // Update the existing contract with the new values
      _context.Entry(existingContract).CurrentValues.SetValues(tblContract);

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TblContractExists(contractId))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }




    [HttpPut("{contractId}")]
    public async Task<IActionResult> PutTblContractByContractId(int contractId, TblContract tblContract)
    {

      //tblContract.ResubmitDate = DateTime.Now;
      // Check if the incoming contract's ID matches the specified contractId
      if (contractId != tblContract.ContractId)
      {
        return BadRequest();
      }

      // Find the existing contract in the database
      var existingContract = await _context.TblContracts.FindAsync(contractId);
      if (existingContract == null)
      {
        return NotFound();
      }

      // Update the existing contract with the new values
      _context.Entry(existingContract).CurrentValues.SetValues(tblContract);

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TblContractExists(contractId))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }


    // POST: api/TblContracts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
        public async Task<ActionResult<TblContract>> PostTblContract(TblContract tblContract)
        {
          if (_context.TblContracts == null)
          {
              return Problem("Entity set 'EcresMreContext.TblContracts'  is null.");
          }

         tblContract.CreatedDate = DateTime.Now;

         _context.TblContracts.Add(tblContract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblContract", new { id = tblContract.ContractId }, tblContract);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusSubmit(int id, [FromBody] TblContract tblContract)
        {
          var existingsample = await _context.TblContracts.Where(x => x.ContractId == id).FirstOrDefaultAsync();
          if (existingsample == null) { return BadRequest(); }

          existingsample.StatusId = 2;
          await _context.SaveChangesAsync();

          return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusDraft(int id, [FromBody] TblContract tblContract)
        {
          var existingsample = await _context.TblContracts.Where(x => x.ContractId == id).FirstOrDefaultAsync();
          if (existingsample == null) { return BadRequest(); }

          existingsample.StatusId = 1;
          await _context.SaveChangesAsync();

          return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusReject(int id, [FromBody] TblContract tblContract)
        {
          var existingsample = await _context.TblContracts.Where(x => x.ContractId == id).FirstOrDefaultAsync();
          if (existingsample == null) { return BadRequest(); }

          existingsample.StatusId = 1;
          existingsample.UpdatedDate = DateTime.Now;
          await _context.SaveChangesAsync();

          return NoContent();
        }




    [HttpPut("{id}")]
    public async Task<IActionResult> PutStatusReject1(int id, [FromBody] TblContract tblContract)
    {
      // Find the existing contract with the specified ID
      var existingContract = await _context.TblContracts
          .Where(x => x.ContractId == id && x.StatusId == 1)
          .FirstOrDefaultAsync();

      // Validate the contract
      if (existingContract == null)
      {
        return BadRequest("No valid contract found.");
      }

      // Ensure ContractDate is not null or empty
      if (string.IsNullOrEmpty(existingContract.ContractDate))
      {
        return BadRequest("Contract date is missing.");
      }

      // Try parsing the contract date
      if (!DateTime.TryParse(existingContract.ContractDate, out DateTime contractDate))
      {
        return BadRequest("Invalid contract date format.");
      }

      // Calculate 14 days from the contract date
      DateTime dateLimit = contractDate.AddDays(14);
      DateTime currentDate = DateTime.Now;

      // Ensure the contract is within 14 days and in the same month
      if (currentDate < contractDate || currentDate > dateLimit || currentDate.Month != contractDate.Month)
      {
        return BadRequest("Contract is outside the allowed timeframe.");
      }

      // Update contract details
      existingContract.StatusId = 1;
      existingContract.UpdatedDate = DateTime.Now;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
      }

      return NoContent();
    }



    // DELETE: api/TblContracts/5
    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblContract(int id)
        {
            if (_context.TblContracts == null)
            {
                return NotFound();
            }
            var tblContract = await _context.TblContracts.FindAsync(id);
            if (tblContract == null)
            {
                return NotFound();
            }

            _context.TblContracts.Remove(tblContract);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblContractExists(int id)
        {
            return (_context.TblContracts?.Any(e => e.ContractId == id)).GetValueOrDefault();
        }
    }
}
