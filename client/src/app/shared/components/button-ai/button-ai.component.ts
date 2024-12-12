import { Component, Input } from "@angular/core";
import { CommonModule } from "@angular/common";

@Component({
	selector: "app-button-ai",
	standalone: true,
	imports: [CommonModule],
	templateUrl: "./button-ai.component.html",
	styleUrls: ["./button-ai.component.scss"],
})
export class ButtonAiComponent {
	@Input() _title: string = "Button A";
	title: string = "";
	constructor() {
		this.title = this._title;
	}
}
