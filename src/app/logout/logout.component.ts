import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../auth/auth.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit{

  constructor(private router: Router, private toastr : ToastrService,public authService: AuthService,public dialog: MatDialog) { }
  
  ngOnInit(): void {
   
  }

  logout(){
    this.authService.changeIsLoggedIn(false);
    // this.authService.changeUser(null);
    this.router.navigate(['home']);
    this.toastr.success('Logout successful');

    this.authService.changeIsLoggedIn(false);
    // this.authService.changeUser({} as userDTO);
    this.router.navigate(['/']);
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