import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserManual } from '../../models/userManual';
import { CompanyService } from '../../Company/company.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-edit-user-manual',
  templateUrl: './edit-user-manual.component.html',
  styleUrls: ['./edit-user-manual.component.css']
})
export class EditUserManualComponent {

  userManual: UserManual = {} as UserManual;
  file: File | null = null;

  constructor(
    public dialogRef: MatDialogRef<EditUserManualComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserManual,
    private companyService: CompanyService,
    private toastr: ToastrService
  ) {
    this.userManual = { ...data }; // Load the data into the form
  }

  handleFileInput(event: any) {
    this.file = event.target.files[0];
    this.userManual.userManual = this.file ? this.file.name : ''; // Update the file name
  }

  onSubmit(): void {
    if (this.file) {
      this.companyService.PutTblUploadUserManuals(this.userManual.id, this.userManual).subscribe({
        next: () => {
          this.companyService.PostFile(this.userManual.id, this.file).subscribe({
            next: () => {
              this.toastr.success('User manual updated successfully', 'Success');
              this.dialogRef.close(true); // Close dialog and indicate success
            },
            error: (fileError) => {
              console.error('Error uploading file:', fileError);
              this.toastr.error('Failed to upload new file', 'Error');
            }
          });
        },
        error: (error) => {
          console.error('Error updating user manual:', error);
          this.toastr.error('Failed to update user manual', 'Error');
        }
      });
    } else {
      this.toastr.warning('Please select a file to upload.', 'Warning');
    }
  }

  onCancel(): void {
    this.dialogRef.close(); 
  }
}
