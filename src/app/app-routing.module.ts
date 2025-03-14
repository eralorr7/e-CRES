import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisteredContractComponent } from './ContractRegistration/registered-contract/registered-contract.component';
import { AddContractComponent } from './ContractRegistration/add-contract/add-contract.component';
import { ViewRegContractComponent } from './ContractRegistration/view-reg-contract/view-reg-contract.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { DraftComponent } from './ContractRegistration/draft/draft.component';
import { UpdateRegContractComponent } from './ContractRegistration/update-reg-contract/update-reg-contract.component';
import { AddCompanyComponent } from './Company/add-company/add-company.component';
import { CompanyListComponent } from './Company/company-list/company-list.component';
import { ViewCompanyComponent } from './Company/view-company/view-company.component';
import { AddUserManualComponent } from './UserManual/add-user-manual/add-user-manual.component';
import { ShowUserManualComponent } from './UserManual/show-user-manual/show-user-manual.component';
import { UpdateCompanyComponent } from './Company/update-company/update-company.component';
import { ListContractComponent } from './list-contract/list-contract.component';
import { RejectContractComponent } from './reject-contract/reject-contract.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

const routes: Routes = [

  {
    path : "ecres", component:HomeComponent, pathMatch: "full"
  },

  {
    path:'home',
    component: HomeComponent
  },
  
  {
    path:'addContract',
    component: AddContractComponent
  },

  {
    path:'registeredContract',
    component: RegisteredContractComponent
  },

  {
    path:'listContract',
    component: ListContractComponent
  },

  {
    path:'viewRegContract',
    component: ViewRegContractComponent
  },

  {
    path:'login',
    component: LoginComponent
  },

  {
    path:'logout',
    component: LogoutComponent
  },

  {
    path:'changePassword',
    component: ChangePasswordComponent
  },

  {
    path:'draft',
    component: DraftComponent
  },

  {
    path: 'view-reg-contract/:contractId',
    component: ViewRegContractComponent
  },

  {
    path: 'update-reg-contract/:contractId',
    component: UpdateRegContractComponent
  },

  {
    path:'addCompany',
    component: AddCompanyComponent
  },

  {
    path:'companyList',
    component: CompanyListComponent
  },

  {
    path:'view-company/:companyId',
    component: ViewCompanyComponent
  },

  {
    path:'addUserManual',
    component: AddUserManualComponent
  },

  {
    path:'showUserManual',
    component: ShowUserManualComponent
  },

  {
    path: 'update-company/:companyId',
    component: UpdateCompanyComponent
  },

  {
    path:'rejectContract',
    component: RejectContractComponent
  },

  {
    path:'forgotPassword',
    component: ForgotPasswordComponent
  },

  {
    path:'resetPassword',
    component: ResetPasswordComponent
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

