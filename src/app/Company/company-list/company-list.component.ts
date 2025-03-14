import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import { Company } from 'src/app/models/company';
import { CompanyService } from '../company.service';
import { ToastrService } from 'ngx-toastr';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.css']
})
export class CompanyListComponent implements OnInit {

  displayedColumns: string[] = [
    'companyId',
    // 'bil',
    'companyName',
    'pic',
    // 'email',
    'email1',
    'username',
    // 'password',
    'status',
    'action'
  ];

  dataSource!: MatTableDataSource<Company>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private companyService: CompanyService,
    private router: Router,
    public authService: AuthService,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.companyService.GetTblCompanies().subscribe(data => {
      this.dataSource = new MatTableDataSource<Company>(data);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


  view(companyId: number) {
    this.router.navigate(['view-company/' + companyId]);
  }


  update(companyId: number) {
    this.router.navigate(['update-company/' + companyId]);
  }


  submit() {
    this.companyService.SendEmailsToCompanies().subscribe({
      next: (response: { message: string }) => { 
        console.log('Emails sent successfully:', response.message);
        this.toastr.success(response.message, 'Success');
      },
      error: (err) => {
        console.error('Error occurred:', err);
        this.toastr.error('Failed to send some or all emails. Check console for details.', 'Error');
      }
    });
  }


  onStatusChange(row: any): void {
    this.companyService.updateCompanyStatus(row.companyId, row.status).subscribe({
      next: () => {
        this.toastr.success('Status updated successfully', 'Success');
      },
      error: (err) => {
        console.error('Error updating status:', err);
        this.toastr.error('Failed to update status', 'Error');
        row.status = !row.status; // Revert the checkbox state
      }
    });
  }


  exportToExcel(): void {
    const columnMappings: Record<string, string> = {
      companyName: 'Company Name',
      licenseNo: 'License No',
      type: 'Type',
      add1: 'Address 1',
      add2: 'Address 2',
      add3: 'Address 3',
      postcode: 'Postcode',
      town: 'Town',
      state: 'State',
      rubberGrade: 'Rubber Grade',
      contactNumber: 'ContactNo',
      telNo: 'Tel No',
      faxNo: 'Fax No',
      pic: 'Pic',
      email: 'Email',
      email1: 'Email 1',
      updatedOn: 'Updated On',

    };
    this.dataSource.data.forEach(row => {
      row.companyName = row.companyName || 'N/A';
      row.licenseNo = row.licenseNo || 'N/A';
      row.type = row.type || 'N/A';
      row.add1 = row.add1 || 'N/A';
      row.add2 = row.add2 || 'N/A';
      row.add3 = row.add3 || 'N/A';
      // row.quantity = row.quantity || 'N/A';
      row.postcode = row.postcode || 'N/A';
      row.town = row.town || 'N/A';
      row.state = row.state || 'N/A';
      row.rubberGrade = row.rubberGrade || 'N/A';
      row.contactNumber = row.contactNumber || 'N/A';
      row.telNo = row.telNo || 'N/A';
      row.faxNo = row.faxNo || 'N/A';
      row.pic = row.pic || 'N/A';
      row.email = row.email || 'N/A';
      row.email1 = row.email1 || 'N/A';
      row.updatedOn = row.updatedOn || 'N/A';

    });
    // Extract columns based on the mapping
    const columnsToExport = Object.keys(columnMappings);
    // Create a new array to hold the filtered data
    const filteredData = this.dataSource.data.map(row => {
      const filteredRow: any = {};
      columnsToExport.forEach(column => {
        // Use column mappings to assign the values dynamically
        // if (column === 'contractDate') {
        //   filteredRow[columnMappings[column]] = row.exportedContractDate; // Use formatted date
        // }
        // // else if (column === 'createdDate') {
        // //   filteredRow[columnMappings[column]] = row.exportedCreatedDate; // Use formatted date
        // // }
        // else {
        filteredRow[columnMappings[column]] = row[column as keyof typeof row];
        // }
      });
      return filteredRow;
    });
    console.log('Filtered Data:', filteredData); // Debug log
    // Convert the filtered data to an Excel worksheet
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(filteredData);
    // Create a new workbook and append the worksheet
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Registered Contract');
    // Export the workbook to an Excel file
    XLSX.writeFile(wb, 'Company_List.xlsx');
  }


}
