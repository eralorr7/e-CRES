import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Ecres } from 'src/app/models/ecres';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DeleteComponent } from 'src/app/delete/delete.component';
import { AuthService } from 'src/app/auth/auth.service';
import { Company } from 'src/app/models/company';
import * as XLSX from 'xlsx';
import { ContractRegistrationService } from '../ContractRegistration/contract-registration.service';
import { CompanyService } from '../Company/company.service';
import * as _moment from 'moment';
@Component({
  selector: 'app-list-contract',
  templateUrl: './list-contract.component.html',
  styleUrls: ['./list-contract.component.css']
})
export class ListContractComponent implements OnInit {

  displayedColumns: string[] = [
    'contractId',
    'contractType',
    'companyName',
    'contractNo',
    'contractDate',
    'destination',
    'shipmentType',
    'action',
  ];
  dataSource!: MatTableDataSource<Ecres>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private contractRegistrationService: ContractRegistrationService,
    private router: Router,
    private dialog: MatDialog,
    public authService: AuthService,
    private companyService: CompanyService,
  ) { }

  company: Company = {} as Company;
  searchedby: string = '';
  searchBy: string[] = ['Contract Type', 'Contract No', 'Shipment Type', 'Company Name', 'Type of Trade', 'Type of Rubber/Grade', 'Shipment Term', 'Contract Date', 'Delivery Month']; // Add your options here
  showContractType: boolean = false
  showContractNo: boolean = false
  showShipmentType: boolean = false
  showCompanyName: boolean = false
  showTypeOfTrade: boolean = false
  showRubberType: boolean = false
  showShipmentTerm: boolean = false
  showContractDate: boolean = false
  showDeliveryMonth: boolean = false
  ecres: Ecres = {} as Ecres;
  ecreses: Ecres[] = [];
  shipments: any;
  contractTypes: any;
  companies: any;
  filters = { contractNo: '', contractType: '', shipmentType: '', companyName: '', trade: '', rubberType: '', shipmentTerm: '', contractDate: '' };
  startDate: Date | null = null;
  endDate: Date | null = null;
  rubberTypes: any;
  shipmentTerms: any;

  ngOnInit(): void {
    this.loadContracts();
    this.fetchContractTypes();
    this.fetchShipments();
    this.companyService.GetTblCompanies().subscribe(data => {
      this.companies = data.sort((a, b) =>
        a.companyName.localeCompare(b.companyName)
      );
    });


    this.contractRegistrationService.GetTblRubberTypes().subscribe(data => {
      this.rubberTypes = data
    })

    this.contractRegistrationService.GetTblShipmentTerms().subscribe(data => {
      this.shipmentTerms = data
    })
  }


  chosenYearHandler(normalizedMonth: _moment.Moment, datepicker: any) {
    const ctrlValue = _moment().month(normalizedMonth.month()).year(normalizedMonth.year());
    this.ecres.month1 = ctrlValue.format('MM/YYYY');
    this.filterByMonthRange();
    datepicker.close();
  }


  chosenYearHandler2(normalizedMonth: _moment.Moment, datepicker: any) {
    const ctrlValue = _moment().month(normalizedMonth.month()).year(normalizedMonth.year());
    this.ecres.month2 = ctrlValue.format('MM/YYYY');
    this.filterByMonthRange();
    datepicker.close();
  }


  clearMonthFilters() {
    this.ecres.month1 = '';
    this.ecres.month2 = '';
    this.loadContracts();
  }


  filterByMonthRange(): void {
    // If month1 is empty, don't filter
    if (!this.ecres.month1) {
      return;
    }

    // Convert month1 to the start of the month for comparison
    const selectedStartMonth = _moment(this.ecres.month1, 'MM/YYYY').startOf('month').format('MM/YYYY');

    // If month2 is not selected or empty, only filter by month1
    if (!this.ecres.month2) {
      const filteredEcres = this.ecreses.filter((ecres: Ecres) => {
        const ecresMonth1 = _moment(ecres.month1, 'MM/YYYY').format('MM/YYYY');

        // Include only records where month1 matches the selected month1 or is greater
        return ecresMonth1 === selectedStartMonth;
      });

      // Update the data source with the filtered records
      this.dataSource = new MatTableDataSource<Ecres>(filteredEcres);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      return;
    }

    // If both month1 and month2 are selected, filter by the range
    const selectedEndMonth = _moment(this.ecres.month2, 'MM/YYYY').endOf('month').format('MM/YYYY');

    const filteredEcres = this.ecreses.filter((ecres: Ecres) => {
      const ecresMonth1 = _moment(ecres.month1, 'MM/YYYY').format('MM/YYYY');
      const ecresMonth2 = _moment(ecres.month2, 'MM/YYYY').format('MM/YYYY');

      // Filter by both month1 and month2
      return (ecresMonth1 >= selectedStartMonth && ecresMonth1 <= selectedEndMonth) &&
        (ecresMonth2 >= selectedStartMonth && ecresMonth2 <= selectedEndMonth);
    });

    // Update the data source with the filtered records
    this.dataSource = new MatTableDataSource<Ecres>(filteredEcres);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }



  filterByContractDate(): void {
    const filteredEcres = this.ecreses.filter(ecres => {
      const contractDate = new Date(ecres.contractDate);
      const contractDateWithoutTime = new Date(contractDate.setHours(0, 0, 0, 0)).getTime();  // Set time to 00:00:00 for comparison

      // Handle startDate and endDate, ensuring both can be the same
      const startDateTime = this.startDate ? new Date(this.startDate).setHours(0, 0, 0, 0) : null;
      const endDateTime = this.endDate ? new Date(this.endDate).setHours(0, 0, 0, 0) : null;

      // Case 1: If startDate and endDate are the same, only match that exact date
      if (startDateTime && endDateTime && startDateTime === endDateTime) {
        return contractDateWithoutTime === startDateTime;  // Only match the exact date
      }

      // Case 2: Use date range filter (inclusive)
      const isStartDateValid = startDateTime ? contractDateWithoutTime >= startDateTime : true;
      const isEndDateValid = endDateTime ? contractDateWithoutTime <= endDateTime : true;

      return isStartDateValid && isEndDateValid;
    });

    this.dataSource = new MatTableDataSource<Ecres>(filteredEcres);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }



  loadContracts() {
    this.contractRegistrationService.GetTblContracts().subscribe(data => {
      this.ecreses = data; // Store the full list of contracts
      this.dataSource = new MatTableDataSource<Ecres>(this.ecreses);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }


  fetchContractTypes() {
    this.contractRegistrationService.GetTblContractTypes().subscribe(data => {
      this.contractTypes = data;
    });
  }


  fetchShipments() {
    this.contractRegistrationService.GetTblShipments().subscribe(data => {
      this.shipments = data;
    });
  }


  changeContractType(event: Event) {
    const contractType = (event.target as HTMLSelectElement).value;
    if (contractType) {
      const filteredEcres = this.ecreses.filter((ecres: Ecres) =>
        ecres.contractType && // Check if contractType is not null or undefined
        ecres.contractType.toString().toLowerCase().includes(contractType.toLowerCase())
      );
      console.log(contractType);
      this.dataSource = new MatTableDataSource<Ecres>(filteredEcres);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    } else {
      this.loadContracts();
    }
  }


  changeContractNo(event: Event) {
    const contractNo = (event.target as HTMLSelectElement).value;
    if (contractNo) {
      const filteredEcres = this.ecreses.filter((ecres: Ecres) =>
        ecres.contractNo && // Check if contractNo is not null or undefined
        ecres.contractNo.toString().toLowerCase() === contractNo.toLowerCase()
      );
      this.dataSource = new MatTableDataSource<Ecres>(filteredEcres);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    } else {
      this.loadContracts();
    }
  }


  changeShipmentType(event: Event) {
    const shipmentType = (event.target as HTMLSelectElement).value;
    if (shipmentType) {
      const filteredEcres = this.ecreses.filter((ecres: Ecres) =>
        ecres.shipmentType && // Ensure shipmentType is not null
        ecres.shipmentType.toString().toLowerCase() === shipmentType.toLowerCase()
      );
      this.dataSource = new MatTableDataSource<Ecres>(filteredEcres);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    } else {
      this.loadContracts();
    }
  }

  changeTypeOfTrade(event: Event) {
    const trade = (event.target as HTMLSelectElement).value;
    console.log('Selected Trade:', trade);
    if (trade) {
      const filteredEcres = this.ecreses.filter((ecres: Ecres) =>
        ecres.trade &&
        ecres.trade.toString().toLowerCase() === trade.toLowerCase()
      );
      this.dataSource = new MatTableDataSource<Ecres>(filteredEcres);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }

    else {
      this.loadContracts();
    }
  }


  changeRubberType(event: Event) {
    const rubberType = (event.target as HTMLSelectElement).value;
    if (rubberType) {
      const filteredEcres = this.ecreses.filter((ecres: Ecres) =>
        ecres.rubberType && // Ensure shipmentType is not null
        ecres.rubberType.toString().toLowerCase() === rubberType.toLowerCase()
      );
      this.dataSource = new MatTableDataSource<Ecres>(filteredEcres);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    } else {
      this.loadContracts();
    }
  }


  changeShipmentTerm(event: Event) {
    const shipmentTerm = (event.target as HTMLSelectElement).value;
    if (shipmentTerm) {
      const filteredEcres = this.ecreses.filter((ecres: Ecres) =>
        ecres.shipmentTerm && // Ensure shipmentType is not null
        ecres.shipmentTerm.toString().toLowerCase() === shipmentTerm.toLowerCase()
      );
      this.dataSource = new MatTableDataSource<Ecres>(filteredEcres);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    } else {
      this.loadContracts();
    }
  }


  changeCompanyName(event: Event) {
    const companyName = (event.target as HTMLSelectElement).value; // Cast event target to HTMLSelectElement
    if (companyName && this.ecreses) {
      const filteredEcres = this.ecreses.filter((ecres: Ecres) =>
        ecres.companyName && ecres.companyName.toString().toLowerCase().includes(companyName.toLowerCase())
      );
      this.dataSource = new MatTableDataSource<Ecres>(filteredEcres);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    } else {
      // If no contract type is selected, load all contracts
      this.loadContracts();
    }
  }


  resetFilters() {
    this.filters = { contractNo: '', contractType: '', shipmentType: '', companyName: '', trade: '', rubberType: '', shipmentTerm: '', contractDate: '' };
    this.dataSource = new MatTableDataSource<Ecres>(this.ecreses); // Reset data source to original
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.showContractType = false;
    this.showContractNo = false;
    this.showShipmentType = false;
    this.showCompanyName = false;
    this.showTypeOfTrade = false;
    this.showRubberType = false;
    this.showShipmentTerm = false;
    this.showContractDate = false;
    this.showDeliveryMonth = false;
    this.startDate = null;
    this.endDate = null;
  }
  applyFilter(field: keyof typeof this.filters, event: Event) {
    const input = event.target as HTMLInputElement | null;
    const value = input ? input.value : '';
    // Reset other filters except the one being updated
    this.filters = {
      contractNo: '',
      contractType: '',
      shipmentType: '',
      companyName: '',
      trade: '',
      rubberType: '',
      shipmentTerm: '',
      contractDate: '',
    };
    this.filters[field] = value;
    // Apply the filter by assigning the filtered data
    this.dataSource.filter = JSON.stringify(this.filters);
    this.dataSource.filterPredicate = (data: any, filter: string) => {
      const filters = JSON.parse(filter);
      return Object.keys(filters).every((key) =>
        filters[key]
          ? data[key as keyof typeof data]
            .toString()
            .toLowerCase()
            .includes(filters[key].toLowerCase())
          : true
      );
    };
  }


  delete(contractId: number): void {
    console.log(contractId);
    const dialogRef = this.dialog.open(DeleteComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        this.contractRegistrationService.DeleteTblContract(contractId).subscribe({
          next: () => { },
          error: (error) => {
            console.error('Error deleting sample from Elims table:', error);
          }
        });
      }
    });
  }


  view(contractId: number) {
    this.router.navigate(['view-reg-contract/' + contractId]);
  }


  exportToExcel(): void {
    const columnMappings: Record<string, string> = {
      contractId: 'Contract Id',
      trade: 'Type of Trade',
      contractType: 'Contract Type',
      companyName: 'Company Name',
      contractNo: 'Contract No',
      contractDate: 'Contract Date',
      shipmentType: 'Shipment Type',
      month1: 'Delivery Month (From)',
      month2: 'Delivery Month (To)',
      buyerSeller: 'Buyer/Seller',
      rubberType: 'Rubber Type', // ✅ Corrected key
      remarksCentrifugedLatex: 'Remarks Centrifuged Latex',
      quantity: 'Quantity',
      currency: 'Currency',
      price: 'Price',
      priceEquivalent: 'Price Equivalent',
      shipmentTerm: 'Shipment Term',
      otherTerm: 'Other Term',
      placeFactoryPort: 'Place Factory Port',
      destination: 'Destination',
      createdDate: 'Created Date',
      updatedDate: 'Rejected Date',
      resubmitDate: 'Resubmission Date',
    };

    // ✅ Process Data Before Exporting
    const processedData = this.dataSource.data.map(row => ({
      contractId: row.contractId || 'N/A',
      trade: row.trade || 'N/A',
      contractType: row.contractType || 'N/A',
      companyName: row.companyName || 'N/A',
      contractNo: row.contractNo || 'N/A',
      contractDate: row.contractDate ? this.formatDate(new Date(row.contractDate)) : 'N/A',
      shipmentType: row.shipmentType || 'N/A',
      month1: row.month1 || 'N/A',
      month2: row.month2 || 'N/A',
      buyerSeller: row.buyerSeller || 'N/A',
      rubberType: Array.isArray(row.rubberType) ? row.rubberType.join(', ') : row.rubberType || 'N/A', // ✅ Fixed key
      remarksCentrifugedLatex: row.remarksCentrifugedLatex || 'N/A',
      quantity: row.quantity || 'N/A',
      currency: row.currency || 'N/A',
      price: row.price || 'N/A',
      priceEquivalent: row.priceEquivalent || 'N/A',
      shipmentTerm: row.shipmentTerm || 'N/A',
      otherTerm: row.otherTerm || 'N/A',
      placeFactoryPort: row.placeFactoryPort || 'N/A',
      destination: row.destination || 'N/A',
      createdDate: row.createdDate || 'N/A',
      updatedDate: row.updatedDate || 'N/A',
      resubmitDate: row.resubmitDate || 'N/A',
      // createdDate: row.createdDate ? this.formatDate(new Date(row.createdDate)) : 'N/A',
      // updatedDate: row.updatedDate ? this.formatDate(new Date(row.updatedDate)) : 'N/A',
      // resubmitDate: row.resubmitDate ? this.formatDate(new Date(row.resubmitDate)) : 'N/A',
    }));

    console.log('Processed Data:', processedData); // ✅ Debugging log

    // ✅ Create a worksheet and rename columns based on `columnMappings`
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(processedData);
    const header = Object.keys(columnMappings).map(key => columnMappings[key]); // ✅ Get readable column headers
    ws['!ref'] = XLSX.utils.encode_range({ s: { r: 0, c: 0 }, e: { r: processedData.length, c: Object.keys(columnMappings).length - 1 } });

    // ✅ Add headers manually to the worksheet
    XLSX.utils.sheet_add_aoa(ws, [header], { origin: 'A1' });

    // ✅ Create and download the Excel file
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Registered Contracts');
    XLSX.writeFile(wb, 'Registered_Contract_List.xlsx');
  }



  // Function to format dates to dd/mm/yyyy
  private formatDate(date: Date): string {
    const d = new Date(date);
    const day = String(d.getDate()).padStart(2, '0');
    const month = String(d.getMonth() + 1).padStart(2, '0'); // Months are 0-based
    const year = d.getFullYear();
    return `${day}/${month}/${year}`;
  }


  changeSearchBy(event: Event) {
    this.resetFilters(); // Reset filters when search option changes
    const selectedValue = (event.target as HTMLSelectElement).value;
    // Show the relevant filter based on the selected search option
    if (selectedValue === 'Contract Type') {
      this.showContractType = true;
      this.showContractNo = false;
      this.showShipmentType = false;
      this.showCompanyName = false;
    } else if (selectedValue === 'Contract No') {
      this.showContractType = false;
      this.showContractNo = true;
      this.showShipmentType = false;
      this.showCompanyName = false;

    } else if (selectedValue === 'Shipment Type') {
      this.showContractType = false;
      this.showContractNo = false;
      this.showShipmentType = true;
      this.showCompanyName = false;
    } else if (selectedValue === 'Company Name') {
      this.showContractType = false;
      this.showContractNo = false;
      this.showShipmentType = false;
      this.showCompanyName = true;
    } else if (selectedValue === 'Type of Trade') {
      this.showContractType = false;
      this.showContractNo = false;
      this.showShipmentType = false;
      this.showCompanyName = false;
      this.showTypeOfTrade = true;
    } else if (selectedValue === 'Type of Rubber/Grade') {
      this.showContractType = false;
      this.showContractNo = false;
      this.showShipmentType = false;
      this.showCompanyName = false;
      this.showTypeOfTrade = false;
      this.showRubberType = true;
    }
    else if (selectedValue === 'Shipment Term') {
      this.showContractType = false;
      this.showContractNo = false;
      this.showShipmentType = false;
      this.showCompanyName = false;
      this.showTypeOfTrade = false;
      this.showRubberType = false;
      this.showShipmentTerm = true;
    } else if (selectedValue === 'Contract Date') {
      this.showContractType = false;
      this.showContractNo = false;
      this.showShipmentType = false;
      this.showCompanyName = false;
      this.showTypeOfTrade = false;
      this.showRubberType = false;
      this.showShipmentTerm = false;
      this.showContractDate = true;
    }
    else if (selectedValue === 'Delivery Month') {
      this.showContractType = false;
      this.showContractNo = false;
      this.showShipmentType = false;
      this.showCompanyName = false;
      this.showTypeOfTrade = false;
      this.showRubberType = false;
      this.showShipmentTerm = false;
      this.showContractDate = false;
      this.showDeliveryMonth = true;
    }
  }

}
