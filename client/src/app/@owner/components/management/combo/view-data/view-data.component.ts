import { Component, Inject } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Category } from "src/app/core/models/interfaces/Category";
import { Combo } from "src/app/core/models/interfaces/Combo";
import { MatButtonModule } from "@angular/material/button";

@Component({
	selector: "app-view-data",
	standalone: true,
	imports: [CommonModule, MatButtonModule],
	templateUrl: "./view-data.component.html",
	styleUrls: ["./view-data.component.scss"],
})
export class ViewDataComponent {
	constructor(
		public dialogRef: MatDialogRef<ViewDataComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: { listCombo: any[] }
	) {}

	onNoClick(): void {
		this.dialogRef.close(false);
	}
}
