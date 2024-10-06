import {
	Component,
	EventEmitter,
	Inject,
	Input,
	OnInit,
	Output,
} from "@angular/core";
import { CommonModule } from "@angular/common";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { MatButtonModule } from "@angular/material/button";

import {
	AbstractControl,
	FormControl,
	FormGroup,
	NonNullableFormBuilder,
	ValidatorFn,
	Validators,
	ReactiveFormsModule,
	FormsModule,
	MinLengthValidator,
	FormBuilder,
} from "@angular/forms";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzSelectModule } from "ng-zorro-antd/select";
import { StoreService } from "src/app/core/services/store/store.service";
import { Store } from "src/app/core/models/interfaces/Store";
import { ToastrService } from "ngx-toastr";

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
		// contructor dialog
		public dialogRef: MatDialogRef<FormEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: { id: number },
		private fb: NonNullableFormBuilder,
		private storeService: StoreService,
		private toastr: ToastrService,
	) {
		this.validateForm = this.fb.group({
			id: [this.data.id],
			name: ["", [Validators.required]],
			address: ["", [ Validators.required]],
			phone: ["", [Validators.required]],
			IdUser: ["", [Validators.required]],
		});
	}

	ngOnInit(): void {
		if (this.data.id != undefined) {
			this.formAdd = false;
			console.log("Data", this.data.id);
			this.loadForm();
		}
	}

	loadForm(): void {
		this.storeService.getById(this.data.id).subscribe({
			next: (res) => {
				const store = res.data;
				console.log('Data:', store);
				const userId = store.userDTO && store.userDTO.id ? store.userDTO.id : "";
				this.validateForm.setValue({
					id: store.id,
					name: store.name,
					address: store.address,
					phone: store.phone,
					IdUser: userId,
				});
			},
		});
	}

	onNoClick(): void {
		this.dialogRef.close(false);
	}

	onSubmit(): void {
		console.log(this.validateForm.value);
		this.storeService.update(this.validateForm.value).subscribe({
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
