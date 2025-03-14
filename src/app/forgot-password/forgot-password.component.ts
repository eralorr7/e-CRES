import { Component } from '@angular/core';

import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {
  username: string = '';
  email1: string = '';
  isLoading: boolean = false;

  constructor(
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  onSubmit() {
    if (!this.username && !this.email1) {
      this.toastr.warning('Please enter either username or email.');
      return;
    }

    this.isLoading = true;

    this.authService.forgotPassword(this.username, this.email1).subscribe(
      response => {
        this.isLoading = false;
        this.toastr.success('Password reset link has been sent to your email.');
        this.router.navigate(['/login']);
      },
      error => {
        this.isLoading = false;
        const errorMessage = error.error?.message || error.message || 'An error occurred while sending the reset email.';
        this.toastr.error(errorMessage);
      }
    );
  }

}
