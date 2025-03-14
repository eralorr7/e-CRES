import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Company } from 'src/app/models/company';
import { CompanyService } from '../company.service';

@Component({
  selector: 'app-view-company',
  templateUrl: './view-company.component.html',
  styleUrls: ['./view-company.component.css']
})
export class ViewCompanyComponent {
  company: Company = {} as Company;
  companyId: number = 0;

  constructor(
    private route: ActivatedRoute,
    private companyService: CompanyService,
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.companyId = +params['companyId']; // Convert string to number
      this.GetTblCompany(this.companyId); // Fetch the company details
    });
  }

  GetTblCompany(companyId: number): void {
    this.companyService.GetTblCompanyByCompanyId(companyId).subscribe({
      next: (data: Company) => {
        this.company = data;
      },
      error: (error) => {
        console.error('Error fetching company details:', error);
      }
    });
  }

  goBack() {
    window.history.back();
  }

}
