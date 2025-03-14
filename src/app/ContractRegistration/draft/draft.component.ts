import { Component, OnInit, ViewChild } from '@angular/core';
import { ContractRegistrationService } from '../contract-registration.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Ecres } from 'src/app/models/ecres';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DeleteComponent } from 'src/app/delete/delete.component';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-draft',
  templateUrl: './draft.component.html',
  styleUrls: ['./draft.component.css']
})
export class DraftComponent implements OnInit {

  displayedColumns: string[] = [
    // 'id',
    'contractId',
    'contractType',
    'companyName',
    'contractNo',
    'contractDate',
    'action',
  ];

  dataSource!: MatTableDataSource<Ecres>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private contractRegistrationService: ContractRegistrationService, private router: Router, private dialog: MatDialog, public authService: AuthService,) { }


  ngOnInit(): void {

    this.authService.currentUser.subscribe(company => {
      this.contractRegistrationService.GetDraftTblContractByCompanyId(company.companyId).subscribe(data => {
        console.log(company.companyId)
        this.dataSource = new MatTableDataSource<Ecres>(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      })
    });
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


  delete(contractId: number): void {
    console.log(contractId);
    const dialogRef = this.dialog.open(DeleteComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        this.contractRegistrationService.DeleteTblContract(contractId).subscribe({
          next: () => {
            // Update the table data
            this.dataSource.data = this.dataSource.data.filter((item: any) => item.contractId !== contractId);

            console.log(`Contract with ID ${contractId} deleted successfully.`);
          },
          error: (error) => {
            console.error('Error deleting contract:', error);
          }
        });
      }
    });
  }


  view(contractId: number) {
    this.router.navigate(['view-reg-contract/' + contractId]);
  }


  update(contractId: number) {
    this.router.navigate(['update-reg-contract/' + contractId]);
  }

}
