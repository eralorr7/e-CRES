import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Ecres } from '../models/ecres';
import { AuthService } from '../auth/auth.service';
import { CompanyService } from '../Company/company.service';
import { ContractRegistrationService } from '../ContractRegistration/contract-registration.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-reject-contract',
  templateUrl: './reject-contract.component.html',
  styleUrls: ['./reject-contract.component.css']
})
export class RejectContractComponent implements OnInit {

  rejectedContracts = new Set<number>();

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
    public authService: AuthService,
    private companyService: CompanyService,
    private toastr: ToastrService
  ) { }

  ecres: any;
  contractTypes: any;
  companies: any;
  filters = {
    contractNo: '',
    contractType: '',
    companyName: '',
    companyId: ''
  };

  ngOnInit(): void {

    this.contractRegistrationService.GetTblContractByStatus(2).subscribe(data => {
      this.ecres = data;
    });

    this.contractRegistrationService.GetTblContractTypes().subscribe(z => {
      this.contractTypes = z
    })

    this.companyService.GetTblCompanies().subscribe(data => {
      this.companies = data.sort((a, b) =>
        a.companyName.localeCompare(b.companyName)
      );
    });

  }


  applyFilters() {
    // Start with the full dataset
    let filteredData = [...this.ecres];

    // Apply the required contractNo filter
    if (this.filters.contractNo) {
      filteredData = filteredData.filter(contract =>
        contract.contractNo.toLowerCase() === this.filters.contractNo.toLowerCase()
      );
    }

    // Apply optional contractType filter
    if (this.filters.contractType) {
      filteredData = filteredData.filter(contract =>
        contract.contractType === this.filters.contractType
      );
    }

    // Apply the companyName filter
    if (this.filters.companyName) {
      filteredData = filteredData.filter(contract =>
        contract.companyName.toLowerCase().includes(this.filters.companyName.toLowerCase())
      );
    }
    this.dataSource = new MatTableDataSource(filteredData);
  }



  view(contractId: number) {
    this.router.navigate(['view-reg-contract/' + contractId]);
  }



  reject(contractId: number) {
    if (this.rejectedContracts.has(contractId)) {
      return;  // Prevent duplicate rejection
    }

    this.contractRegistrationService.PutStatusReject(contractId).subscribe(
      (response) => {
        this.toastr.success('Contract successfully rejected.', 'Success');
        this.rejectedContracts.add(contractId);  // Mark as rejected

        // Automatically remove the rejected contract from the displayed list
        this.dataSource.data = this.dataSource.data.filter(
          contract => contract.contractId !== contractId
        );
      },
      (error) => {
        this.toastr.error('Failed to reject the contract. Please try again.', 'Error');
      }
    );
  }


}
