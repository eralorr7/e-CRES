import { Component, OnInit } from '@angular/core';
import { UserManual } from '../../models/userManual';
import { CompanyService } from '../../Company/company.service';
import { AuthService } from '../../auth/auth.service';
import { environment } from '../../environments/environment';
import { EditUserManualComponent } from '../edit-user-manual/edit-user-manual.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-add-user-manual',
  templateUrl: './add-user-manual.component.html',
  styleUrls: ['./add-user-manual.component.css']
})
export class AddUserManualComponent implements OnInit {
  userManual: UserManual = {} as UserManual;
  file: any | null;
  userManuals: UserManual[] = [];

  constructor(
    private companyService: CompanyService,
    public authService: AuthService,
    public dialog: MatDialog 
  ) { }


  ngOnInit(): void {
    this.fetchUserManuals();
  }


  handleFileInput(event: any) {
    this.file = event.target.files[0];
    this.userManual.userManual = event.target.files[0].name;
    console.log(this.userManual.userManual);
  }


  fetchUserManuals(): void {
    this.companyService.GetTblUploadUserManuals().subscribe({
      next: (data) => {
        this.userManuals = data;
      },
      error: (error) => {
        console.error('Error fetching user manuals:', error);
      }
    });
  }


  viewFile(userManual: UserManual, path: string): void {
    window.open(environment.baseApiUrlUserManual + userManual.id + "_" + path, "_blank");
  }


  editUserManual(userManual: UserManual): void {
    const dialogRef = this.dialog.open(EditUserManualComponent, {
      width: '400px',
      data: userManual // Pass the user manual data to the modal
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.fetchUserManuals(); // Refresh the list if the dialog was closed with a success indication
      }
    });
  }
}
