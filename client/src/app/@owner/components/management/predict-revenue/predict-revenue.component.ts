import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ChartData, ChartOptions, ChartType } from "chart.js";
import { NgChartsModule } from "ng2-charts";

@Component({
	selector: "app-predict-revenue",
	standalone: true,
	imports: [CommonModule, NgChartsModule],
	templateUrl: "./predict-revenue.component.html",
	styleUrls: ["./predict-revenue.component.scss"],
})
export class PredictRevenueComponent {
	listRevenue: any[] = ["92.75", "604.21", "457.2", "405.85", "403.7"];
	public lineChartData: ChartData<"line"> = {
		labels: [
			"Mon, 04 Dec 2023",
			"Tue, 05 Dec 2023",
			"Wed, 06 Dec 2023",
			"Thu, 07 Dec 2023",
			"Fri, 08 Dec 2023",
		],
		datasets: [{ data: this.listRevenue, label: "Thu nhập dự đoán" }],
	};
	public lineChartOptions: ChartOptions = {
		responsive: true,
	};
	public lineChartType: ChartType = "line";
}
