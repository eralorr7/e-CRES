import { Component, OnInit, ViewChild } from '@angular/core';
import { ContractRegistrationService } from '../contract-registration.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Ecres } from 'src/app/models/ecres';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-registered-contract',
  templateUrl: './registered-contract.component.html',
  styleUrls: ['./registered-contract.component.css']
})

export class RegisteredContractComponent implements OnInit {

  displayedColumns: string[] = [
    'contractId',
    'contractType',
    'companyName',
    'contractNo',
    'contractDate',
    'action'
  ];

  dataSource!: MatTableDataSource<Ecres>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private contractRegistrationService: ContractRegistrationService,
    private router: Router,
    public authService: AuthService
  ) { }


  ngOnInit(): void {

    this.authService.currentUser.subscribe(company => {
      this.contractRegistrationService.GetTblContractByCompanyId(company.companyId).subscribe(data => {
        console.log(company.companyId);
        this.dataSource = new MatTableDataSource<Ecres>(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
    });
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
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
      rubberTypesString: 'Rubber Type', // ✅ Corrected mapping for rubberTypes
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
    const processedData = this.dataSource.data.map(row => {
      return {
        contractId: row.contractId || 'N/A',
        trade: row.trade || 'N/A',
        contractType: row.contractType || 'N/A',
        companyName: row.companyName || 'N/A',
        contractNo: row.contractNo || 'N/A',
        contractDate: row.contractDate
          ? this.formatDate(new Date(row.contractDate))
          : 'N/A',
        shipmentType: row.shipmentType || 'N/A',
        month1: row.month1 || 'N/A',
        month2: row.month2 || 'N/A',
        buyerSeller: row.buyerSeller || 'N/A',
        rubberTypesString: Array.isArray(row.rubberTypes)
          ? row.rubberTypes.join(', ')
          : row.rubberTypes || 'N/A',
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
      };
    });
  
    console.log('Processed Data:', processedData); // Debug log
  
    // ✅ Create Excel File
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(processedData);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Registered Contract');
    XLSX.writeFile(wb, 'Registered_Contract_List.xlsx');
  }
  

  
  // format dates to dd/mm/yyyy
  private formatDate(date: Date): string {
    const d = new Date(date);
    const day = String(d.getDate()).padStart(2, '0');
    const month = String(d.getMonth() + 1).padStart(2, '0'); // Months are 0-based
    const year = d.getFullYear();
    return `${day}/${month}/${year}`;
  }

}
