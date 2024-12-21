import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
	name: "formatText",
	standalone: true,
})
export class FormatTextPipe implements PipeTransform {
	transform(value: string): string {
		if (!value) {
			return "";
		}

		// Xử lý in đậm (**text**)
		value = value.replace(/\*\*(.*?)\*\*/g, "<strong>$1</strong>");

		// Xử lý xuống dòng (\n)
		value = value.replace(/\n/g, "<br>");

		return value;
	}
}
