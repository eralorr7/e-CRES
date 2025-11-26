import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { Company } from '../models/company';
import { ContractRegistrationService } from '../ContractRegistration/contract-registration.service';
import { UserMre } from '../models/userMre';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  // pipe = new DatePipe('en-US');
  today = new Date();
  company: Company = {} as Company;
  userMre: UserMre = {} as UserMre;
  role: string = '';

  constructor(private authService: AuthService, public dialog: MatDialog, private toastr: ToastrService, private contractRegistrationService: ContractRegistrationService,) { }


  ngOnInit(): void {
    // Get the current role
    this.authService.currentRole.subscribe(role => {
      this.role = role;

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
  }

}
