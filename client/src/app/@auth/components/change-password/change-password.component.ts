import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { ToastrService } from "ngx-toastr";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";

@Component({
    selector: "app-change-password",
    templateUrl: "./change-password.component.html",
    styleUrls: ["./change-password.component.scss"],
	imports: [
		CommonModule,
		FormsModule,
		MatFormFieldModule,
		MatInputModule,
		MatButtonModule,
		MatIconModule,
	  ],
    standalone: true,
})
export class ChangePasswordComponent  {
	email: string = '';
	oldPassword: string = '';
	newPassword: string = '';
	confirmPassword: string = '';
	isSubmitting: boolean = false;
  
	showOldPassword: boolean = false;
	showNewPassword: boolean = false;
	showConfirmPassword: boolean = false;
	constructor(
	  private authService:  AuthenticationService,
	  private toastr: ToastrService,
	){}
	onSubmit(){
	  if (this.newPassword !== this.confirmPassword) {
		this.toastr.error('Mật khẩu xác nhận không khớp', 'Thất bại');
		return;
	  }
	  this.isSubmitting = true;
	  this.authService
		.changePassword(this.email, this.oldPassword, this.newPassword, this.confirmPassword)
		.subscribe({
		  next: (response) => {
			if (response.isSuccess) {
			  this.toastr.success(response.message, 'Thành công');
			  this.resetForm();
			  
			} else {
			  this.toastr.error(response.message, 'Thất bại');
			}
		  },
		  error: (err) => {
			console.error(err);
			this.toastr.error('Đã xảy ra lỗi', 'Thất bại');
		  },
		  complete: () => {
			this.isSubmitting = false;
		  }
		});
	}
	resetForm() {
	  this.email = '';
	  this.oldPassword = "";
	  this.newPassword = '';
	  this.confirmPassword = '';
	}

}
