import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'e-CRES';
  isLoggin: boolean = false;
  loginPage: boolean = true;
  isAuthRoute: boolean = false;

  constructor(private authService: AuthService, private router: Router) {
    // Subscribe to the authentication status
    this.authService.currentLoggedIn.subscribe(res => {
      this.isLoggin = res;
      console.log('Current logged in status:', res);
    });

    // Subscribe to router events to track the current route
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.updateRouteState(event.urlAfterRedirects);
      }
    });
  }

  ngOnInit(): void {
    // Initialize route state
    this.updateRouteState(this.router.url);
  }

  // Update route-related properties
  private updateRouteState(url: string): void {
    const authRoutes = ['/forgotPassword', '/resetPassword'];
    const baseRoute = url.split('?')[0]; // Extract the base route without query parameters
    this.isAuthRoute = authRoutes.includes(baseRoute);

    // Set `loginPage` logic
    this.loginPage = !this.isLoggin && !this.isAuthRoute;
  }
}
