import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { LayoutComponent } from './layout/layout.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatRadioModule } from '@angular/material/radio';
import { RegisteredContractComponent } from './ContractRegistration/registered-contract/registered-contract.component';
import { FormsModule } from '@angular/forms';
import { AddContractComponent } from './ContractRegistration/add-contract/add-contract.component';
import { ViewRegContractComponent } from './ContractRegistration/view-reg-contract/view-reg-contract.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ToastrModule } from 'ngx-toastr';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { DeleteComponent } from './delete/delete.component';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { DraftComponent } from './ContractRegistration/draft/draft.component';
import { UpdateRegContractComponent } from './ContractRegistration/update-reg-contract/update-reg-contract.component';
import { AddCompanyComponent } from './Company/add-company/add-company.component';
import { CompanyListComponent } from './Company/company-list/company-list.component';
import { ViewCompanyComponent } from './Company/view-company/view-company.component';
import { AddUserManualComponent } from './UserManual/add-user-manual/add-user-manual.component';
import { ShowUserManualComponent } from './UserManual/show-user-manual/show-user-manual.component';
import { UpdateCompanyComponent } from './Company/update-company/update-company.component';
import { EditUserManualComponent } from './UserManual/edit-user-manual/edit-user-manual.component';
import { ListContractComponent } from './list-contract/list-contract.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE, MatNativeDateModule } from '@angular/material/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatMomentDateModule, MomentDateAdapter } from '@angular/material-moment-adapter';
import { RejectContractComponent } from './reject-contract/reject-contract.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';



export const MY_FORMATS = {
  parse: {
    dateInput: 'MM/YYYY',
  },
  display: {
    dateInput: 'MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@NgModule({
  declarations: [
    AppComponent,
    SidenavComponent,
    ToolbarComponent,
    LayoutComponent,
    HomeComponent,
    RegisteredContractComponent,
    AddContractComponent,
    ViewRegContractComponent,
    LoginComponent,
    LogoutComponent,
    DeleteComponent,
    ChangePasswordComponent,
    DraftComponent,
    UpdateRegContractComponent,
    AddCompanyComponent,
    CompanyListComponent,
    ViewCompanyComponent,
    AddUserManualComponent,
    ShowUserManualComponent,
    UpdateCompanyComponent,
    EditUserManualComponent,
    ListContractComponent,
    RejectContractComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatFormFieldModule,
    MatRadioModule,
    FormsModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    HttpClientModule,
    MatDialogModule,
    ToastrModule.forRoot(),
    MatSelectModule,
    MatInputModule,
    MatIconModule,
    MatTooltipModule,
    MatButtonModule,
    BrowserModule,
    MatDatepickerModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatMomentDateModule,
    MatTooltipModule,
    MatSnackBarModule,

  ],
  providers: [
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
