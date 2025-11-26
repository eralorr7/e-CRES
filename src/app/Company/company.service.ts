import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Company } from '../models/company';
import { lastValueFrom, Observable } from 'rxjs';
import { UserManual } from '../models/userManual';
import { license } from '../models/license';
import { premise } from '../models/premise';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  baseApiUrl: string = environment.baseApiUrl;
  baseUrlicenselAdmin: string = environment.baseUrlicenselAdmin;

  constructor(private http: HttpClient) { }

  GetTblCompanies() {
    return this.http.get<Company[]>(this.baseApiUrl + '/api/TblCompanies/GetTblCompanies');
  }

  PostTblCompany(company: Company): Observable<Company> {
    return this.http.post<Company>(this.baseApiUrl + '/api/TblCompanies/PostTblCompany1', company);
  }

  GetTblCompanyByCompanyId(companyId: number) {
    return this.http.get<Company>(this.baseApiUrl + '/api/TblCompanies/GetTblCompanyByCompanyId/' + companyId);
  }

  GetTblStates() {
    return this.http.get<Company>(this.baseApiUrl + '/api/TblStates/GetTblStates');
  }

  PostUserManual(userManual: UserManual): Observable<UserManual> {
    return this.http.post<UserManual>(this.baseApiUrl + '/api/TblUploadUserManuals/PostTblUploadUserManual', userManual);
  }

  PostFile(id: any, file: any) {
    const formData = new FormData();
    formData.append("file", file);
    console.log(file)
    return this.http.post(this.baseApiUrl + '/api/TblUploadUserManuals/UploadUserManual/' + id, formData);
  }

  GetTblUploadUserManuals() {
    return this.http.get<UserManual[]>(this.baseApiUrl + '/api/TblUploadUserManuals/GetTblUploadUserManuals');
  }

  PutTblUploadUserManuals(id: number, userManual: UserManual) {
    return this.http.put<UserManual>(this.baseApiUrl + '/api/TblUploadUserManuals/PutTblUploadUserManual/' + id, userManual);
  }

  PutTblCompanyByCompanyId(companyId: number, tblCompany: Company) {
    return this.http.put<Company>(this.baseApiUrl + '/api/TblCompanies/PutTblCompanyByCompanyId/' + companyId, tblCompany);
  }

  SendEmailsToCompanies(): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(
      `${this.baseApiUrl}/api/Email/SendEmailsToCompanies/sendEmailsToCompanies`,
      {}
    );
  }

  updateCompanyStatus(companyId: number, status: boolean): Observable<any> {
    return this.http.put(`${this.baseApiUrl}/api/TblCompanies/updateCompanyStatus/${companyId}/status`, status);
  }

  async getLesen(licenseNo: any): Promise<any> {
    return lastValueFrom(this.http.get<license>(this.baseUrlicenselAdmin + '/Licenses/GetLicense/' + licenseNo));
  }



  async getPremiseMyLesenByID(id: any): Promise<premise> {
    return lastValueFrom(this.http.get<premise>(this.baseUrlicenselAdmin + '/Premises/GetPremise/' + id));
  }


  SendPasswordEmail(email1: string, username: string, plainPassword: string): Observable<void> {
    const emailPayload = {
      email1: email1,
      username: username,
      plainPassword: plainPassword, // Ensure 'PlainPassword' is passed and matches the backend's expected name
    };
    return this.http.post<void>(this.baseApiUrl + '/api/TblCompanies/SendPasswordEmail1/SendPasswordEmail', emailPayload);
  }


}
