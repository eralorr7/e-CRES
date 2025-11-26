import { Component, OnInit } from '@angular/core';
import { Company } from '../../models/company';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import { CompanyService } from '../company.service';
import { license } from 'src/app/models/license';
import { premise } from 'src/app/models/premise';

@Component({
  selector: 'app-add-company',
  templateUrl: './add-company.component.html',
  styleUrls: ['./add-company.component.css']
})
export class AddCompanyComponent implements OnInit {

  constructor(
    private companyService: CompanyService,
    private toastr: ToastrService,
    private router: Router,
    public authService: AuthService,
  ) { }

  company: Company = {} as Company;
  errorMes: boolean = false;
  isSubmitting = false;
  licenseNo: string = '';
  licenseData: license | null = null;
  errorMessage: string = '';
  premiseData: premise | null = null;


  ngOnInit(): void { }


  validForm() {
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
  
    // Check  all required fields are filled
    if (
      !this.company.pic || 
      !this.company.email1 || 
      !this.company.username || 
      this.company.status == null
    ) {
      this.toastr.warning('Please fill in all fields');
      return false;
    }
  
    // Check  the email contains multiple addresses separated by ',' or ';'
    if (/[,;]/.test(this.company.email1)) {
      this.toastr.warning('The email1 field must contain only one email address');
      return false;
    }
  
    // Check  email address format is valid
    if (!emailPattern.test(this.company.email1)) {
      this.toastr.warning('Please enter a valid email address');
      return false;
    }
  
    return true; // Form is valid
  }
  

  onSubmit() {
    if (!this.validForm()) {
      this.errorMes = true;
    } else {
      this.errorMes = false;
      this.isSubmitting = true;
  

      this.companyService.PostTblCompany(this.company).subscribe({
        next: (response) => {
          console.log('Company created successfully:', response);
          this.toastr.success('Company added successfully!', 'Success');
          this.router.navigate(['/companyList']);
        },
        error: (err) => {
          console.error('Error occurred while creating company:', err);
  
          // Check if the error status is 409 (Conflict)
          if (err.status === 409) {
            // Handle conflict for username already exists
            if (err.error.message.includes('Username')) {
              this.toastr.error('Username already exists. Please choose another username.', 'Error');
            }
            // Handle conflict for license number already exists
            else if (err.error.message.includes('License number')) {
              this.toastr.error('License number already exists.', 'Error');
            } else {
              this.toastr.error('Error occurred during submission.', 'Error');
            }
          } else {
            this.toastr.error('Error occurred during submission.', 'Error');
          }
  
          this.isSubmitting = false; // Reset submitting flag
        },
        complete: () => {
          this.isSubmitting = false; // Reset submitting flag after operation
        }
      });
    }
  }
  

  async searchLicense(): Promise<void> {
    this.errorMessage = '';
    this.licenseData = null;
    this.premiseData = null;

    if (!this.licenseNo.trim()) {
      this.errorMessage = 'Please enter a valid license number.';
      return;
    }

    try {
      // Fetch License Data
      this.licenseData = await this.companyService.getLesen(this.licenseNo);
      if (!this.licenseData) {
        this.errorMessage = 'No data found for the provided license number.';
        return;
      }

      // Fetch Premise Data using Premise ID from License Data
      this.premiseData = await this.companyService.getPremiseMyLesenByID(
        this.licenseData.premiseId
      );
      if (!this.premiseData) {
        this.errorMessage = 'No data found for the associated premise.';
      }

      this.company.companyName = this.licenseData.companyName;
      this.company.licenseNo = this.licenseData.licenseNo;
      this.company.licenseCategory = this.licenseData.licenseCategory;
      this.company.contactNumber = this.premiseData.mobileNo;
      this.company.telNo = this.premiseData.phoneNo;
      this.company.faxNo = this.premiseData.fax;
      this.company.add1 = this.premiseData.add1;
      this.company.add2 = this.premiseData.add2;
      this.company.add3 = this.premiseData.add3;
      this.company.postcode = this.premiseData.postcode;
      this.company.town = this.premiseData.town;
      this.company.state = this.premiseData.state;
      this.company.email = this.premiseData.email;


    } catch (error) {
      this.errorMessage = 'An error occurred while fetching the data.';
      console.error(error);
    }
  }


}
