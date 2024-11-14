import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NgChartsModule } from "ng2-charts";
import { ChartOptions, ChartType, ChartDataset, ChartData } from "chart.js";
import { BaseChartDirective } from "ng2-charts";
@Component({
	selector: "app-analytics",
	standalone: true,
	imports: [CommonModule, NgChartsModule],
	templateUrl: "./analytics.component.html",
	styleUrls: ["./analytics.component.scss"],
})
export class AnalyticsComponent {
	// Dữ liệu cho biểu đồ đường
	public lineChartData: ChartData<"line"> = {
		labels: [
			"January",
			"February",
			"March",
			"April",
			"May",
			"June",
			"July",
		],
		datasets: [
			{ data: [65, 59, 80, 81, 56, 55, 40], label: "Series A" },
			{ data: [28, 48, 40, 19, 86, 27, 90], label: "Series B" },
		],
	};
	public lineChartOptions: ChartOptions = {
		responsive: true,
	};
	public lineChartType: ChartType = "line";

	// Dữ liệu cho biểu đồ tròn
	public pieChartData: ChartData<"pie", number[], string> = {
		labels: ["Download Sales", "In-Store Sales", "Mail Sales"],
		datasets: [{ data: [300, 500, 100] }],
	};
	public pieChartType: ChartType = "pie";

	// Dữ liệu cho biểu đồ thanh
	public barChartOptions: ChartOptions = {
		responsive: true,
	};
	public barChartData: ChartData<"bar"> = {
		labels: ["2006", "2007", "2008", "2009", "2010", "2011", "2012"],
		datasets: [
			{ data: [65, 59, 80, 81, 56, 55, 40], label: "Series A" },
			{ data: [28, 48, 40, 19, 86, 27, 90], label: "Series B" },
		],
	};
	public barChartType: ChartType = "bar";
}
