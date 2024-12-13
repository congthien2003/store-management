import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ChartData, ChartOptions, ChartType } from "chart.js";
import { NgChartsModule } from "ng2-charts";
import { AiPredictService } from "src/app/core/services/store/ai-predict.service";
import { Store } from "src/app/core/models/interfaces/Store";

@Component({
	selector: "app-predict-revenue",
	standalone: true,
	imports: [CommonModule, NgChartsModule],
	templateUrl: "./predict-revenue.component.html",
	styleUrls: ["./predict-revenue.component.scss"],
})
export class PredictRevenueComponent implements OnInit {
	store!: Store;
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
	constructor(private AIService: AiPredictService) {}
	ngOnInit(): void {
		this.store = JSON.parse(
			sessionStorage.getItem("storeInfo") ?? ""
		) as Store;
		this.AIService.predict(this.store.id).subscribe({
			next: (res) => {
				console.log(res);
				let arrayDate: any[] = [];
				res.forEach((element: any) => {
					this.listRevenue.push(element.revenue);

					arrayDate.push(element.date);
				});
				this.lineChartData = {
					labels: arrayDate,
					datasets: [
						{ data: this.listRevenue, label: "Thu nhập dự đoán" },
					],
				};
			},
		});
	}
}
