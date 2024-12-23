import { Component, Inject } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { MatButtonModule } from "@angular/material/button";
import { TicketService } from "src/app/core/services/ticket.service";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzSelectModule } from "ng-zorro-antd/select";
import { AWSService } from "src/app/core/services/aws.service";
import { UserService } from "src/app/core/services/user/user.service";
import { Store } from "src/app/core/models/interfaces/Store";
import {
	FormGroup,
	FormBuilder,
	Validators,
	ReactiveFormsModule,
} from "@angular/forms";
const NzModule = [NzFormModule, NzSelectModule];

@Component({
	selector: "form-add-food",
	standalone: true,
	imports: [CommonModule, MatButtonModule, ReactiveFormsModule, NzModule],
	templateUrl: "./form-add.component.html",
	styleUrls: ["./form-add.component.scss"],
})
export class FormAddComponent {
	validateForm!: FormGroup;
	fb!: FormBuilder;
	constructor(
		public dialogRef: MatDialogRef<FormAddComponent>,
		@Inject(MAT_DIALOG_DATA) public data: { idUser: number },
		private ticketService: TicketService,
		private toastr: ToastrService,
		private awsService: AWSService,
		private userService: UserService
	) {
		this.validateForm = this.fb.group({
			id: [0],
			title: ["", [Validators.required]],
			description: [],
			status: 0,
			created: [new Date()],
			requestBy: data.idUser,
		});
	}
	onNoClick(): void {
		this.dialogRef.close(false);
	}
	onSubmit() {
		this.ticketService.create(this.validateForm.value).subscribe({
			next: (res) => {
				console.log(res);
				if (res.isSuccess) {
					this.toastr.success(res.message, "Thành công", {
						timeOut: 3000,
					});
					this.userService.getById(this.data.idUser).subscribe({
						next: (res) => {
							this.awsService
								.sendMailThanks(res.data.email)
								.subscribe({
									next: (res) => {
										console.log(res.message);
									},
								});
						},
					});
					this.dialogRef.close(true);
				} else {
					this.toastr.error(res.message, "Thất bại", {
						timeOut: 3000,
					});
					this.dialogRef.close(false);
				}
			},
			error: (err) => {
				this.toastr.error(err.message, "Thất bại", {
					timeOut: 3000,
				});
				this.dialogRef.close(false);
			},
		});
	}
}
