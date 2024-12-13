import { Component, Inject } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { MatNativeDateModule } from "@angular/material/core";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import { FormGroup, FormControl, ReactiveFormsModule } from "@angular/forms";

@Component({
	selector: "analytic-datetime-picker",
	standalone: true,
	imports: [
		CommonModule,
		MatFormFieldModule,
		MatInputModule,
		MatDatepickerModule,
		MatNativeDateModule,
		MatButtonModule,
		ReactiveFormsModule,
	],
	templateUrl: "./datetime-picker.component.html",
	styleUrls: ["./datetime-picker.component.scss"],
})
export class DatetimePickerComponent {
	range = new FormGroup({
		start: new FormControl<Date | null>(null),
		end: new FormControl<Date | null>(null),
	});
	constructor(
		public dialogRef: MatDialogRef<DatetimePickerComponent>,
		@Inject(MAT_DIALOG_DATA) public data: {}
	) {}

	onSubmit(): void {
		this.dialogRef.close(this.range.value);
		this.range.reset();
	}

	onNoClick(): void {
		this.dialogRef.close();
	}
}
