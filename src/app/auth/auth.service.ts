import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, from, tap } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Company } from '../models/company';
import { loginDTO } from '../models/loginDTO';
import { ChangePasswordDTO } from '../models/changePasswordDTO';
import { UserMre } from '../models/userMre';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseApiUrl: string = environment.baseApiUrl;


  private userSource = new BehaviorSubject<Company>(null as any);
  private adminUserSource = new BehaviorSubject<UserMre>(null as any);

  currentUser = this.userSource.asObservable();
  currentAdminUser = this.adminUserSource.asObservable();

  private roleSource = new BehaviorSubject<string>(''); // Role indicator
  currentRole = this.roleSource.asObservable();

  private loggedInSource = new BehaviorSubject(false);
  currentLoggedIn = this.loggedInSource.asObservable();

  constructor(private httpclient: HttpClient) { }

  // User login method
  userLogin(object: loginDTO): Observable<Company> {
    const body = JSON.stringify(object);
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.httpclient.post<Company>(`${this.baseApiUrl}/api/TblCompanies/PostLoginEcres/Login`, body, httpOptions)
      .pipe(
        tap((userData: Company) => {
          this.userSource.next(userData);
          this.roleSource.next('user'); // Set role to user
          this.loggedInSource.next(true);
        })
      );
  }

  // Admin login method
  adminLogin(object: loginDTO): Observable<UserMre> {
    const body = JSON.stringify(object);
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.httpclient.post<UserMre>(`${this.baseApiUrl}/api/TblUserMres/PostLogin/Login`, body, httpOptions)
      .pipe(
        tap((adminData: UserMre) => {
          this.adminUserSource.next(adminData);
          this.roleSource.next('admin'); // Set role to admin
          this.loggedInSource.next(true);
        })
      );
  }


  changePassword(changePasswordDTO: ChangePasswordDTO): Observable<any> {
    const body = JSON.stringify(changePasswordDTO);
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };

    return this.httpclient.post<any>(this.baseApiUrl + '/api/TblCompanies/ChangePasswordEcres/ChangePassword', body, httpOptions);
  }


  changeIsLoggedIn(status: boolean) {
    this.loggedInSource.next(status);
  }

  requestPasswordReset(usernameOrEmail: string): Observable<any> {
    return this.httpclient.post<any>(`${this.baseApiUrl}/forgot-password`, { usernameOrEmail });
  }


  forgotPassword(username: string, email1: string): Observable<any> {
    const payload = { username, email1 };
    return this.httpclient.post(`${this.baseApiUrl}/api/TblCompanies/ForgotPassword/ForgotPassword`, payload);
  }
  
    // Reset Password API call
    resetPassword(token: string, newPassword: string): Observable<any> {
      const payload = { token, newPassword };
      return this.httpclient.post(`${this.baseApiUrl}/api/TblCompanies/ResetPassword/ResetPassword`, payload);
    }

}