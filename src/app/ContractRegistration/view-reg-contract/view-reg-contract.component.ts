import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ContractRegistrationService } from '../contract-registration.service';
import { Ecres } from 'src/app/models/ecres';

@Component({
  selector: 'app-view-reg-contract',
  templateUrl: './view-reg-contract.component.html',
  styleUrls: ['./view-reg-contract.component.css']
})
export class ViewRegContractComponent implements OnInit {

  ecres: Ecres = {} as Ecres;
  contractId: number = 0;

  constructor(
    private route: ActivatedRoute,
    private contractRegistrationService: ContractRegistrationService,
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.contractId = +params['contractId']; // Convert string to number
      this.GetTblContract(this.contractId); 
    });
  }


  GetTblContract(contractId: number): void {
    this.contractRegistrationService.GetTblContractByContractId(contractId).subscribe({
      next: (data: Ecres) => {
        this.ecres = data;
        console.log('Ecres:', this.ecres);
      },
      error: (error) => {
        console.error('Error fetching contract:', error);
      }
    });
  }


  goBack() {
    window.history.back();
  }

}
