import { Component, Inject, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { MatButtonModule } from "@angular/material/button";
import {
	FormGroup,
	NonNullableFormBuilder,
	Validators,
	ReactiveFormsModule,
	FormsModule,
} from "@angular/forms";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzSelectModule } from "ng-zorro-antd/select";
import { Ticket } from "src/app/core/models/interfaces/Ticket";
import { ToastrService } from "ngx-toastr";
import { TicketService } from "src/app/core/services/ticket.service";
const NzModule = [NzFormModule, NzSelectModule];
@Component({
	selector: "app-form-edit",
	standalone: true,
	imports: [
		CommonModule,
		MatButtonModule,
		ReactiveFormsModule,
		NzModule,
		FormsModule,
	],
	templateUrl: "./form-edit.component.html",
	styleUrls: ["./form-edit.component.scss"],
})
export class FormEditComponent implements OnInit {
	formAdd: boolean = true;

	validateForm!: FormGroup;

	selectedValue: number = 0;
	constructor(
		public dialogRef: MatDialogRef<FormEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: { id: number },
		private fb: NonNullableFormBuilder,
		private ticketService: TicketService,
		private toastr: ToastrService
	) {
		this.validateForm = this.fb.group({
			id: [this.data.id],
			title: ["", [Validators.required, Validators.minLength(6)]],
			description: [],
			requestBy: [],
			created: [],
			status: [],
		});
	}
	ngOnInit(): void {
		if (this.data.id != undefined) {
			this.formAdd = false;
			this.loadForm();
		}
	}
	loadForm(): void {
		this.ticketService.getById(this.data.id).subscribe({
			next: (res) => {
				const ticket = res.data as Ticket;
				console.log(ticket);

				this.validateForm.setValue({
					id: ticket.id,
					title: ticket.title,
					description: ticket.description,
					requestBy: ticket.requestBy,
					created: ticket.createdAt,
					status: ticket.status,
				});
			},
		});
	}
	onNoClick(): void {
		this.dialogRef.close(false);
	}
	onSubmit(): void {
		this.ticketService.update(1, this.validateForm.value).subscribe({
			next: (res) => {
				if (res.isSuccess) {
					this.toastr.success(res.message, "Thành công", {
						timeOut: 3000,
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
				console.log(err);

				this.toastr.error(err.message.message, "Thất bại", {
					timeOut: 3000,
				});
				this.dialogRef.close(false);
			},
		});
	}
}
