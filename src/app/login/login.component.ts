import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AppComponent } from '../app.component';
import { loginDTO } from '../models/loginDTO';
import { Company } from '../models/company';
import { UserMre } from '../models/userMre';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: loginDTO = {} as loginDTO;
  isProcess: boolean = false;

  constructor(public authService: AuthService, private toastr: ToastrService, private router: Router, public appComponent: AppComponent) { }

  ngOnInit(): void { }


  // login(){
  //   if(
  //       (this.user.username == null || this.user.username == "") ||
  //       (this.user.password == null || this.user.password == "")
  //     )
  //   {
  //     this.user = {} as loginDTO;
  //     this.toastr.warning('Fill in the required information')
  //   }
  //   else{
  //     this.isProcess = true;

  //     this.authService.userLogin(this.user).subscribe(
  //       res =>  {
  //                 if(res != null)
  //                 {
  //                   this.isProcess = false;
  //                   this.authService.changeIsLoggedIn(true);
  //                   // this.authService.changeUser(res);
  //                   this.router.navigate(['home']);
  //                   this.toastr.success('Welcome !');
  //                 }
  //               },
  //       err => {
  //         this.isProcess = false;
  //         this.toastr.warning(err.error);
  //       }
  //     )
  //   }
  // }

  login() {
    if (this.user.username == null || this.user.username == "" || this.user.password == null || this.user.password == "") {
      this.user = {} as loginDTO;
      this.toastr.warning('Fill in the required information');
    } else {
      this.isProcess = true;

      // First attempt user login
      this.authService.userLogin(this.user).subscribe(
        (res: Company) => {
          if (res) {
            this.isProcess = false;
            this.authService.changeIsLoggedIn(true);
            this.router.navigate(['home']); // Navigate to user home page
            this.toastr.success('Welcome');
          }
        },
        err => {
          // If user login fails, try admin login
          this.authService.adminLogin(this.user).subscribe(
            (adminRes: UserMre) => {
              if (adminRes) {
                this.isProcess = false;
                this.authService.changeIsLoggedIn(true);
                this.router.navigate(['home']); // Navigate to admin dashboard
                this.toastr.success('Welcome');
              }
            },
            adminErr => {
              this.isProcess = false;
              this.toastr.warning('Invalid username or password');
            }
          );
        }
      );
    }
  }

  forgotPassword() {
    this.router.navigate(['/forgotPassword']);
  }


}
