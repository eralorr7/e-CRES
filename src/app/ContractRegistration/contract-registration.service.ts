import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Ecres } from '../models/ecres';
import { Observable } from 'rxjs';
import { Month } from '../models/month';
import { Year } from '../models/year';
import { Company } from '../models/company';

@Injectable({
  providedIn: 'root'
})
export class ContractRegistrationService {

  baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }

  GetTblRubberTypes() {
    return this.http.get<Ecres[]>(this.baseApiUrl + '/api/TblRubberTypes/GetTblRubberTypes');
  }

  GetTblShipmentTerms() {
    return this.http.get<Ecres>(this.baseApiUrl + '/api/TblShipmentTerms/GetTblShipmentTerms');
  }

  GetTblShipments() {
    return this.http.get<Ecres>(this.baseApiUrl + '/api/TblShipments/GetTblShipments');
  }

  GetTblContractTypes() {
    return this.http.get<Ecres[]>(this.baseApiUrl + '/api/TblContractTypes/GetTblContractTypes');
  }

  GetTblMonths(): Observable<Month[]> {
    return this.http.get<Month[]>(this.baseApiUrl + '/api/TblMonths/GetTblMonths');
  }

  GetTblYears(): Observable<Year[]> {
    return this.http.get<Year[]>(this.baseApiUrl + '/api/TblYears/GetTblYears');
  }

  GetTblContracts() {
    return this.http.get<Ecres[]>(this.baseApiUrl + '/api/TblContracts/GetTblContracts');
  }

  GetTblContractByStatus(statusId: number) {
    return this.http.get<Ecres[]>(`${this.baseApiUrl}/api/TblContracts/GetTblContractByStatus1?statusId=${statusId}`);
  }

  SearchTblContractByStatus(statusId: number, contractNo: string, contractType?: string, companyName?: string) {
    // Start building the URL with the required parameters
    let params = new HttpParams()
      .set('statusId', statusId.toString())
      .set('contractNo', contractNo);
  
    // Add optional parameters if they are provided
    if (contractType) {
      params = params.set('contractType', contractType);
    }
    if (companyName) {
      params = params.set('companyName', companyName);
    }
  
    // Make the HTTP GET request with query parameters
    return this.http.get<Ecres[]>(`${this.baseApiUrl}/api/TblContracts/SearchTblContractByStatus`, { params });
  }

  DeleteTblContract(contractId: number): Observable<Ecres> {
    return this.http.delete<Ecres>(this.baseApiUrl + '/api/TblContracts/DeleteTblContract/' + contractId);
  }

  PutTblContract(contractId: number, ecres: Ecres) {
    return this.http.put<Ecres>(this.baseApiUrl + '/api/TblContracts/PutDateReceiPutTblContractvedInLab/' + contractId, ecres);
  }

  GetTblContractByCompanyId(companyId: number) {
    return this.http.get<Ecres[]>(this.baseApiUrl + '/api/TblContracts/GetTblContractByCompanyId/' + companyId);
  }

  GetTblContractByContractId(contractId: number) {
    return this.http.get<Ecres>(this.baseApiUrl + '/api/TblContracts/GetTblContractByContractId/' + contractId);
  }

  PutTblContractByContractId1(contractId: number, tblContract: Ecres) {
    return this.http.put<Ecres>(this.baseApiUrl + '/api/TblContracts/PutTblContractByContractId1/' + contractId, tblContract);
  }

  GetDraftTblContractByCompanyId(companyId: number) {
    return this.http.get<Ecres[]>(this.baseApiUrl + '/api/TblContracts/GetDraftTblContractByCompanyId/draft/' + companyId);
  }

  PostTblContract(ecres: Ecres): Observable<Ecres> {
    return this.http.post<Ecres>(this.baseApiUrl + '/api/TblContracts/PostTblContract', ecres);
  }

  GetTblCompanies() {
    return this.http.get<Company>(this.baseApiUrl + '/api/TblCompanies/GetTblCompanies');
  }

  PutStatusSubmit(id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<string>(this.baseApiUrl + '/api/TblContracts/PutStatusSubmit/' + id, httpOptions);
  }

  PutStatusDraft(id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<string>(this.baseApiUrl + '/api/TblContracts/PutStatusDraft/' + id, httpOptions);
  }

  PutStatusReject(id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<string>(this.baseApiUrl + '/api/TblContracts/PutStatusReject/' + id, httpOptions);
  }

}
