import { Component, EventEmitter, Inject, Output } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";

@Component({
	selector: "app-popup-submit-order",
	standalone: true,
	imports: [CommonModule],
	templateUrl: "./popup-submit-order.component.html",
	styleUrls: ["./popup-submit-order.component.scss"],
})
export class PopupSubmitOrderComponent {
	constructor(
		public dialogRef: MatDialogRef<PopupSubmitOrderComponent>,
		@Inject(MAT_DIALOG_DATA) public data: {}
	) {}

	actions(type: string) {
		this.dialogRef.close(type);
	}
}
