import { Component } from '@angular/core';
import { CompanyService } from '../company.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import { Company } from 'src/app/models/company';


@Component({
  selector: 'app-update-company',
  templateUrl: './update-company.component.html',
  styleUrls: ['./update-company.component.css']
})
export class UpdateCompanyComponent {

  constructor(
    private companyService: CompanyService,
    private toastr: ToastrService,
    private router: Router,
    public authService: AuthService,
    private route: ActivatedRoute,
  ) { }

  company: Company = {} as Company;
  states: any;
  errorMes: boolean = false;



  ngOnInit(): void {

    this.route.params.subscribe(params => {
      const companyId = +params['companyId'];
      if (companyId) {
        this.loadContractDetails(companyId);
      }
    });

    this.companyService.GetTblStates().subscribe(data => {
      this.states = data
      console.log(data)
    })

  }


  loadContractDetails(companyId: number) {
    this.companyService.GetTblCompanyByCompanyId(companyId).subscribe(data => {
      this.company = data; 
      console.log(data)
    }, () => {
      this.toastr.error('Error loading contract details');
    });
  }


  // validForm() {
  //   if (
  //     this.company.companyName == null || this.company.companyName == "" ||
  //     this.company.add1 == null || this.company.add1 == "" ||
  //     this.company.email1 == null || this.company.email1 == "" ||
  //     this.company.username == null || this.company.username == "" ||
  //     this.company.password == null || this.company.password == "" ||
  //     this.company.status == null
  //   ) {
  //     this.toastr.warning('Please fill in all fields');
  //     return false;
  //   }
  //   return true;
  // }
  
  
  
  onSubmit() {

    // if (!this.validForm()) {
    //   this.errorMes = true
    // } else {
    //   this.errorMes = false

    const companyId = this.company.companyId;

    this.companyService.PutTblCompanyByCompanyId(companyId, this.company).subscribe({
      next: (response) => {
        console.log('Form submitted successfully', response);
        this.toastr.success('Company updated successfully!', 'Success');
        this.router.navigate(['/companyList']);
      },
      error: (err) => {
        console.error('Error occurred:', err);
        this.toastr.error('Error occurred during submission.', 'Error');
      }
    });
  }
  // }
}
