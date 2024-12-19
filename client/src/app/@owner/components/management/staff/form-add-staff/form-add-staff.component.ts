import { Component, Inject } from "@angular/core";
import { CommonModule } from "@angular/common";
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators,
} from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatInputModule } from "@angular/material/input";
import {
	MatDialogRef,
	MAT_DIALOG_DATA,
	MatDialogModule,
} from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { UserService } from "src/app/core/services/user/user.service";
import { StaffService } from "src/app/core/services/store/staff.service";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { User } from "src/app/core/models/interfaces/User";

@Component({
	selector: "app-form-add-staff",
	standalone: true,
	imports: [
		CommonModule,
		ReactiveFormsModule,
		MatButtonModule,
		MatInputModule,
		MatDialogModule,
	],
	templateUrl: "./form-add-staff.component.html",
	styleUrls: ["./form-add-staff.component.scss"],
})
export class FormAddStaffComponent {
	userForm: FormGroup;
	staffForm: FormGroup;

	constructor(
		private dialogRef: MatDialogRef<FormAddStaffComponent>,
		@Inject(MAT_DIALOG_DATA) public data: { storeId: number },
		private fb: FormBuilder,
		private userService: UserService,
		private staffService: StaffService,
		private toastr: ToastrService
	) {
		this.userForm = this.fb.group({
			username: ["", [Validators.required, Validators.minLength(6)]],
			email: ["", [Validators.required, Validators.email]],
			password: ["", [Validators.required, Validators.minLength(6)]],
			phones: ["", [Validators.required]],
			role: [3], // Staff role
		});

		this.staffForm = this.fb.group({
			name: ["", Validators.required],
			age: ["", [Validators.required, Validators.min(18)]],
			address: ["", Validators.required],
			idStore: [this.data.storeId],
			idUser: [0],
		});
	}

	onSubmit(): void {
		if (this.userForm.valid && this.staffForm.valid) {
			// First create user
			this.userService.create(this.userForm.value as User).subscribe({
				next: (userRes) => {
					if (userRes.isSuccess) {
						// Then create staff with returned userId
						this.staffForm.patchValue({
							idUser: userRes.data.id,
						});

						this.staffService
							.create(this.staffForm.value)
							.subscribe({
								next: (staffRes) => {
									if (staffRes.isSuccess) {
										this.toastr.success(
											"Thêm nhân viên thành công"
										);
										this.dialogRef.close(true);
									} else {
										this.toastr.error(staffRes.message);
									}
								},
								error: (err) => {
									this.toastr.error(
										"Lỗi khi tạo thông tin nhân viên"
									);
								},
							});
					} else {
						this.toastr.error(userRes.message);
					}
				},
				error: (err) => {
					this.toastr.error("Lỗi khi tạo tài khoản");
				},
			});
		}
	}

	onCancel(): void {
		this.dialogRef.close(false);
	}
}
