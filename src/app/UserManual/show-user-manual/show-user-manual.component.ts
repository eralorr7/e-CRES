import { Component } from '@angular/core';
import { environment } from '../../environments/environment';
import { UserManual } from '../../models/userManual';
import { CompanyService } from '../../Company/company.service';

@Component({
  selector: 'app-show-user-manual',
  templateUrl: './show-user-manual.component.html',
  styleUrls: ['./show-user-manual.component.css']
})
export class ShowUserManualComponent {

  constructor(
    private companyService: CompanyService,
  ) { }

  userManual: UserManual = {} as UserManual;
  baseApiUrlUserManual = environment.baseApiUrlUserManual;
  userManuals: any

  ngOnInit(): void {
    this.companyService.GetTblUploadUserManuals().subscribe(data => {
      if (data && data.length > 0) {
        this.userManuals = data[0];  // Select the first item from the array
      }
      console.log(this.userManual);
    });
  }


  viewFile(userManual: UserManual, path: string): void {
    window.open(environment.baseApiUrlUserManual + userManual.id + "_" + path, "_blank");
  }

}
