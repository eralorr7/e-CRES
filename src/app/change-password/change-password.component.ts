import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../auth/auth.service';
import { ChangePasswordDTO } from '../models/changePasswordDTO';
import { Company } from '../models/company';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  changePasswordDTO: ChangePasswordDTO = {} as ChangePasswordDTO;
  company: Company = {} as Company;

  constructor(public authService: AuthService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(res => {
      this.company = res;
      console.log('Logged in user:', res);
    });
  }

  
  changePassword() {
    if (!this.changePasswordDTO.username || !this.changePasswordDTO.oldPassword || !this.changePasswordDTO.newPassword) {
      this.toastr.warning('Please fill in all fields');
      return;
    }

    this.authService.changePassword(this.changePasswordDTO).subscribe(
      res => {
        if (res && res.success) {
          this.toastr.success('Password changed successfully!');
        } else {
          this.toastr.error('Unexpected response from server.');
        }
      },
      err => {
        const errorMessage = err.error?.message || 'Error changing password. Please try again.';
        this.toastr.error(errorMessage);
        console.error(err);
      }
    );
  }


}
