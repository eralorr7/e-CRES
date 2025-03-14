import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent implements OnInit {
  newPassword: string = '';
  confirmPassword: string = '';
  token: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Retrieve the reset token from the query parameters
    this.token = this.route.snapshot.queryParamMap.get('token');
    if (!this.token) {
      alert('Invalid or missing reset token.');
      this.router.navigate(['/']);
    }
  }

  // Function that gets called when the form is submitted
  onSubmit(): void {
    if (this.newPassword !== this.confirmPassword) {
      alert('Passwords do not match!');
      return;
    }

    if (!this.token) {
      alert('Invalid reset token.');
      return;
    }

    // Call the AuthService to reset the password
    this.authService.resetPassword(this.token, this.newPassword).subscribe(
      () => {
        alert('Password reset successfully!');
        this.router.navigate(['/login']);
      },
      (error) => {
        console.error('Error resetting password:', error);
        alert('Error resetting password. Please try again.');
      }
    );
  }
}
