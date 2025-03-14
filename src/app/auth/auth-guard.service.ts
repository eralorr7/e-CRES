import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private authService: AuthService, private router: Router,){}

  canActivate(): boolean {

    let permitted;
    this.authService.currentLoggedIn.subscribe(res => permitted = res);
    
    if(permitted) return true;

    this.router.navigate(['home']);
    return false;
  }

  // canActivate(): boolean {
  //   let permitted;
  //   this.authService.currentLoggedIn.subscribe(res => permitted = res);
  
  //   if (permitted || localStorage.getItem('isLoggedIn') === 'true') {
  //     return true;
  //   }
  
  //   this.router.navigate(['home']);
  //   return false;
  // }
  
}
