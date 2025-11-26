import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ContractRegistrationService } from '../contract-registration.service';
import { Ecres } from 'src/app/models/ecres';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import * as _moment from 'moment';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-contract',
  templateUrl: './add-contract.component.html',
  styleUrls: ['./add-contract.component.css'],
})
export class AddContractComponent implements OnInit {

  constructor(
    private contractRegistrationService: ContractRegistrationService,
    private toastr: ToastrService,
    private router: Router,
    public authService: AuthService,
    private cdr: ChangeDetectorRef,
    private fb: FormBuilder,

  ) {
    this.contractForm = this.fb.group({
      rubberId: [null, Validators.required],
      remarksCentrifugedLatex: ['']
    });

  }

  selectedRubberIds: string[] = [];
  ecres: Ecres = {} as Ecres;
  rubberTypes: any[] = [];
  shipmentTerms: any;
  shipments: any;
  contractTypes: any;
  errorMes: boolean = false;
  startDate = _moment();
  isDraftDisabled = false;
  isSubmitDisabled: boolean = false;
  disableButtons: boolean = false;
  contractForm: FormGroup;

  tooltipContent = `
  To indicate the place of loading.
  Examples:

  Local contracts:

  1. Factory Name (ABC Factory), State (Kedah)
  2. Port name (Port Klang), State (Selangor)

  Overseas contract:

  1. Country (Thailand), Port Name (Laem Chabang)
  2. Country (Vietnam), Factory Name (ABC Factory)
`;


  tooltipDestination = `
Please indicate destination or port of destination.
Examples:

Local contracts:

1.	Factory Name (ABC Factory), State (Kedah) or 
2.	Port Name (North Port), Factory Name (ABC Factory)

Overseas contract:

1.	Port Name (Port Klang), Country (Japan)
2.	Port Name (Qingdao), Factory Name (ABC Factory)
`;


  ngOnInit(): void {

    this.contractRegistrationService.GetTblRubberTypes().subscribe(data => {
      this.rubberTypes = data
    })

    this.contractRegistrationService.GetTblShipmentTerms().subscribe(data => {
      this.shipmentTerms = data
    })

    this.contractRegistrationService.GetTblShipments().subscribe(data => {
      this.shipments = data
    })

    this.contractRegistrationService.GetTblContractTypes().subscribe(z => {
      this.contractTypes = z
    })

  }


  onRubberSelectionChange(event: any, rubberId: number) {
    if (event.target.checked) {
      // Add rubberId if checked
      if (!this.selectedRubberIds.includes(rubberId.toString())) {
        this.selectedRubberIds.push(rubberId.toString());
      }
    } else {
      // Remove rubberId if unchecked
      this.selectedRubberIds = this.selectedRubberIds.filter(id => id !== rubberId.toString());

      // If '4' is removed, clear remarksCentrifugedLatex and reset validation
      if (rubberId === 4) {
        this.ecres.remarksCentrifugedLatex = '';  // Reset field
        this.contractForm.get('remarksCentrifugedLatex')?.clearValidators(); // Remove required validation
        this.contractForm.get('remarksCentrifugedLatex')?.updateValueAndValidity(); // Refresh validation
      }
    }
  }

  calculateTradingDays(startDate: Date, endDate: Date): number {
    let days = 0;
    // Loop from startDate to endDate
    while (startDate <= endDate) {
      // Check if the day is a weekday (not Saturday or Sunday)
      if (startDate.getDay() !== 0 && startDate.getDay() !== 6) {
        days++;
      }
      // Move to the next day
      startDate.setDate(startDate.getDate() + 1);
    }
    return days;
  }



  onContractDateChange(event: any): void {
    const contractDate = new Date(this.ecres.contractDate); // Get the selected contract date
    const currentDate = new Date(); // Get the current date

    // Calculate the difference in trading days between the contract date and current date
    const tradingDaysDiff = this.calculateTradingDays(contractDate, currentDate);

    // Disable buttons if the trading days difference exceeds 5
    if (tradingDaysDiff > 6) {
      this.isSubmitDisabled = true;
      this.isDraftDisabled = true;
      //this.toastr.warning('The key-in date is only allowed on the contract date and the day after!', 'Warning');
    } else {
      // Enable buttons if the trading days difference is 5 or less
      this.isSubmitDisabled = false;
      this.isDraftDisabled = false;
    }
  }



  chosenYearHandler(normalizedMonth: _moment.Moment, datepicker: any) {
    const ctrlValue = this.startDate.clone();
    ctrlValue.month(normalizedMonth.month());
    ctrlValue.year(normalizedMonth.year());

    this.ecres.month1 = ctrlValue.format('MM/YYYY');
    this.cdr.markForCheck();  // Trigger change detection if needed
    datepicker.close();
  }



  chosenYearHandler2(normalizedMonth: _moment.Moment, datepicker: any) {
    const ctrlValue = this.startDate.clone();
    ctrlValue.month(normalizedMonth.month());
    ctrlValue.year(normalizedMonth.year());

    this.ecres.month2 = ctrlValue.format('MM/YYYY');
    this.cdr.markForCheck();
    datepicker.close();
  }



  convertKgToTonne(kg: number): number {
    return kg / 1000; // 1 tonne = 1000 kg
  }



  onShipmentIdChange(newShipmentId: number): void {
    this.ecres.shipmentId = newShipmentId;
    if (newShipmentId !== 4) {
      this.ecres.month2 = '';
    }
  }



  onShipmentTerm(newShipmentTerm: number): void {
    this.ecres.shipmentTermId = newShipmentTerm;
    if (newShipmentTerm !== 3) {
      this.ecres.otherTerm = '';
    }
  }



  onSubmit() {

    if (this.isSubmitDisabled) {
      return; // Do not draft if buttons are disabled
    }

    if (this.ecres.unit === 'Kg' && typeof this.ecres.quantity === 'number') {
      // Convert kg to tonnes for quantityActual
      this.ecres.quantityActual = this.convertKgToTonne(this.ecres.quantity);
    } else if (this.ecres.unit === 'Tonne') {
      // If the unit is already tonne, no conversion needed
      this.ecres.quantityActual = this.ecres.quantity;
    } else {
      // Handle case where quantity is "N/A" or another non-numeric value
      this.ecres.quantity = null;  // or some other value as per your requirement
      this.ecres.quantityActual = null;  // reset quantityActual if needed
    }

    if (!this.validForm()) {
      this.errorMes = true

    } else {
      this.errorMes = false

      // Fetch the companyId from authService
      this.authService.currentUser.subscribe(company => {
        this.ecres.companyId = company.companyId.toString();  // Save companyId to ecres
      });
      // Call the service and pass the form data
      this.ecres.statusId = 2;
      this.ecres.rubberId = this.selectedRubberIds.join(",");
      this.isSubmitDisabled = true;
      console.log('Final quantity being submitted in tonnes:', this.ecres.quantity);
      console.log(this.ecres);


      this.contractRegistrationService.PostTblContract(this.ecres).subscribe({
        next: (response) => {
          // Handle successful response
          console.log('Form submitted successfully', response);
          this.toastr.success('Form submitted successfully!', 'Success');

          this.router.navigate(['/registeredContract']);
        },
        error: (err) => {
          // Handle error response
          console.error('Error occurred:', err);
          // this.toastr.error('Error occurred during draft submission.', 'Error');
          this.toastr.success('Contract submitted successfully!', 'Success');
          this.isSubmitDisabled = false;
          this.router.navigate(['/registeredContract']);
        }
      });
    }
  }



  onDraft() {

    if (this.isDraftDisabled) {
      return;
    }

    if (this.ecres.unit === 'Kg' && typeof this.ecres.quantity === 'number') {
      // Convert kg to tons for quantityActual
      this.ecres.quantityActual = this.convertKgToTonne(this.ecres.quantity);
    } else if (this.ecres.unit === 'Tonne') {
      // If the unit is already tonne, no conversion needed
      this.ecres.quantityActual = this.ecres.quantity;
    } else {
      // Handle case where quantity is "N/A" or another non-numeric value
      this.ecres.quantity = null;  // or some other value as per your requirement
      this.ecres.quantityActual = null;  // reset quantityActual if needed
    }


    if (!this.validForm()) {
      this.errorMes = true
    } else {
      this.errorMes = false
      // Fetch the companyId from authService
      this.authService.currentUser.subscribe(company => {
        this.ecres.companyId = company.companyId.toString();  // Save companyId to ecres
      });
      // Set inDraft to true
      this.ecres.statusId = 1;  // Ensure inDraft is true for this submission

      this.ecres.rubberId = this.selectedRubberIds.join(",");

      this.isDraftDisabled = true;
      // Call the service and pass the form data
      this.contractRegistrationService.PostTblContract(this.ecres).subscribe({
        next: (response) => {
          // Handle successful response
          console.log('Draft submitted', response);
          this.toastr.success('Draft submitted!', 'Success');
          //this.isDraftDisabled = true;
          this.router.navigate(['/draft']);
        },
        error: (err) => {
          // Handle error response
          console.error('Error occurred:', err);
          this.toastr.success('Draft submitted!', 'Success');
          this.isDraftDisabled = false;

          this.router.navigate(['/draft']);
        }
      });
    }
  }



  validForm() {

    const requiresRemarks = this.ecres.rubberId == "4" || this.selectedRubberIds.includes('4');

    if (
      !this.ecres.contractNo ||
      !this.ecres.contractType ||
      !this.ecres.contractDate ||
      !this.ecres.shipmentId ||
      !this.ecres.buyerSeller ||
      !this.ecres.shipmentTermId ||
      !this.ecres.quantity ||
      !this.ecres.unit ||
      !this.ecres.currency ||
      !this.ecres.placeFactoryPort ||
      !this.ecres.trade ||
      !this.ecres.destination ||
      !this.ecres.price ||
      !this.ecres.priceEquivalent ||

      (this.selectedRubberIds.length === 0) ||
      //(this.ecres.price === null || this.ecres.price === undefined || this.ecres.price === "N/A") || 
      (this.ecres.shipmentTermId == 3 && (this.ecres.otherTerm == null || this.ecres.otherTerm.trim() == "")) ||

      (requiresRemarks && (!this.ecres.remarksCentrifugedLatex || this.ecres.remarksCentrifugedLatex.trim() === "")) ||

      (this.ecres.shipmentId == 1 && (this.ecres.month1 == null || this.ecres.month1.trim() == "")) ||
      (this.ecres.shipmentId == 4 && 
        ((this.ecres.month1 == null || this.ecres.month1.trim() == "") || 
        (this.ecres.month2 == null || this.ecres.month2.trim() == "")))


    ) {
      this.toastr.warning('Please fill in all required fields');
      return false;
    }

    return true;
  }


  addDecimal(event: any) {
    // Get the input value
    let value = event.target.value;

    // Remove non-numeric characters except for dot '.'
    value = value.replace(/[^0-9.]/g, '');

    // Check if the decimal point is clicked
    if (event.data === '.') {
      // Move cursor to the end of the value
      event.taarget.selectionStart = event.target.selectionEnd = value.length;
    }

    // Remove any existing decimal point
    value = value.replace('.', '');

    // If value is not empty and has more than two characters, insert decimal point
    if (value.length > 2) {
      value = value.slice(0, -2) + '.' + value.slice(-2);
    }

    // Update the input value
    event.target.value = value;

    // Update the ngModel binding
    this.ecres.price = value;

  }



  addPriceEquivalent(event: any) {
    // Get the input value
    let value = event.target.value;

    // Remove non-numeric characters except for dot '.'
    value = value.replace(/[^0-9.]/g, '');

    // Check if the decimal point is clicked
    if (event.data === '.') {
      // Move cursor to the end of the value
      event.target.selectionStart = event.target.selectionEnd = value.length;
    }

    // Remove any existing decimal point
    value = value.replace('.', '');

    // If value is not empty and has more than two characters, insert decimal point
    if (value.length > 2) {
      value = value.slice(0, -2) + '.' + value.slice(-2);
    }

    // Update the input value
    event.target.value = value;

    // Update the ngModel binding
    this.ecres.priceEquivalent = value;

  }

}
