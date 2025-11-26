import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { LogoutComponent } from '../logout/logout.component';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { Company } from '../models/company';
import { UserMre } from '../models/userMre';
import { UserManual } from '../models/userManual';
import { environment } from '../environments/environment';
import { CompanyService } from '../Company/company.service';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent {

  company: Company = {} as Company;
  userMre: UserMre = {} as UserMre;
  role: string = '';


  userManuals: any
  userManual: UserManual = {} as UserManual;
  baseApiUrlUserManual = environment.baseApiUrlUserManual;

  constructor(private router: Router, private toastr: ToastrService, public authService: AuthService, public dialog: MatDialog, private companyService: CompanyService,) { }


  ngOnInit(): void {
    // Get the current role
    this.authService.currentRole.subscribe(role => {
      this.role = role;

      // Depending on the role, subscribe to the correct observable
      if (role === 'user') {
        this.authService.currentUser.subscribe(res => {
          this.company = res;
          // console.log('Logged in user (Company):', res);
        });
      } else if (role === 'admin') {
        this.authService.currentAdminUser.subscribe(res => {
          this.userMre = res;
          // console.log('Logged in admin (UserMre):', res);
        });
      }
    });

    this.companyService.GetTblUploadUserManuals().subscribe(data => {
      if (data && data.length > 0) {
        this.userManuals = data[0];  // Select the first item from the array
      }
      console.log(this.userManual);
    });

  }



  viewFile(userManual: UserManual, path: string): void {
    window.open(environment.baseApiUrlUserManual + userManual.id + "_" + path, "_blank");
  }


  openLogoutDialog(): void {
    const dialogRef = this.dialog.open(LogoutComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Handle the logout logic here if the user confirms logout
      }
    });
  }

}
