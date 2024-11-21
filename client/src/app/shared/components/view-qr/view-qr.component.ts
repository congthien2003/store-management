import { Component, Inject, InjectionToken, Input } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";

@Component({
	selector: "app-view-qr",
	standalone: true,
	imports: [CommonModule],
	templateUrl: "./view-qr.component.html",
	styleUrls: ["./view-qr.component.scss"],
})
export class ViewQrComponent {
	constructor(
		@Inject(MAT_DIALOG_DATA)
		public data: { url: string },
		public dialogRef: MatDialogRef<ViewQrComponent>
	) {}

	onNoClick(): void {
		this.dialogRef.close();
	}
}
