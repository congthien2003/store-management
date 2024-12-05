import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";

import { NgChartsModule } from "ng2-charts";
import { ChartOptions, ChartType, ChartDataset, ChartData } from "chart.js";
import { BaseChartDirective } from "ng2-charts";
import { AnalystReportService } from "src/app/core/services/store/analyst-report.service";
import { Store } from "src/app/core/models/interfaces/Store";
import { PricePipe } from "src/app/core/utils/price.pipe";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatNativeDateModule } from "@angular/material/core";
import { FormControl } from "@angular/forms";
import { ButtonDownloadComponent } from "src/app/shared/components/button-download/button-download.component";
import { MatButtonModule } from "@angular/material/button";
import { MatMenuModule } from "@angular/material/menu";
const MatImport = [
	MatDatepickerModule,
	MatInputModule,
	MatFormFieldModule,
	MatNativeDateModule,
	MatMenuModule,
	MatButtonModule,
];

@Component({
	selector: "app-analytics",
	standalone: true,
	imports: [
		CommonModule,
		NgChartsModule,
		PricePipe,
		MatImport,
		ButtonDownloadComponent,
	],
	templateUrl: "./analytics.component.html",
	styleUrls: ["./analytics.component.scss"],
})
export class AnalyticsComponent implements OnInit {
	store!: Store;
	promises: Promise<any>[] = [];
	listRevenue = [];
	listMonth = [];
	foodCount!: number;
	tableFree!: number;
	revenueDaily!: number;
	orderDaily!: number;
	date = new FormControl(new Date());
	selectDateFilter: string = "Today only";

	constructor(private analystReportService: AnalystReportService) {}
	ngOnInit(): void {
		this.store = JSON.parse(
			sessionStorage.getItem("storeInfo") ?? ""
		) as Store;
		const dateTime = Date.now();
		const formattedDate = new Date(dateTime)
			.toLocaleDateString("vi-VN", {
				day: "2-digit",
				month: "2-digit",
				year: "numeric",
			})
			.toString();
		console.log(formattedDate);

		const year = new Date().getFullYear();
		this.analystReportService
			.getRevenueMonth(this.store.id, year)
			.subscribe({
				next: (response) => {
					const data = response.data;
					this.listRevenue = data.map((item: any) => item.total);
					this.listMonth = data.map(
						(item: any) => `Tháng ${item.month}`
					);
					this.lineChartData.labels = this.listMonth;
					this.lineChartData.datasets[0].data = this.listRevenue;
				},
				error: (err) =>
					console.error("Error fetching daily revenue", err),
			});
		this.analystReportService
			.getCountFood(this.store.id, formattedDate)
			.subscribe({
				next: (response) => {
					if (response.isSuccess) {
						this.foodCount = response.data;
					} else {
						console.error(
							"Failed to fetch data:",
							response.message
						);
					}
				},
				error: (err) => {
					console.log(err);
				},
			});
		this.analystReportService.getTableFree(this.store.id).subscribe({
			next: (response) => {
				if (response.isSuccess) {
					this.tableFree = response.data;
				}
			},
			error: (err) => {
				console.log(err);
			},
		});
		this.analystReportService
			.getDailyRevenue(this.store.id, formattedDate)
			.subscribe({
				next: (response) => {
					if (response.isSuccess) {
						this.revenueDaily = response.data;
						console.log(this.revenueDaily);
					}
				},
				error: (err) => {
					console.log(err);
				},
			});
		this.analystReportService
			.getCountOrder(this.store.id, formattedDate)
			.subscribe({
				next: (response) => {
					if (response.isSuccess) {
						this.orderDaily = response.data;
						console.log(this.revenueDaily);
					}
				},
				error: (err) => {
					console.log(err);
				},
			});
	}

	// Dữ liệu cho biểu đồ đường
	public lineChartData: ChartData<"line"> = {
		labels: [this.listMonth],
		datasets: [{ data: this.listRevenue, label: "Thu nhập" }],
	};
	public lineChartOptions: ChartOptions = {
		responsive: true,
	};
	public lineChartType: ChartType = "line";
	updateChart(): void {
		// Cập nhật labels và datasets sau khi có dữ liệu
		this.lineChartData.labels = this.listMonth;
		this.lineChartData.datasets[0].data = this.listRevenue;
	}
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

	onExport() {
		// TODO: Implement export logic here
		console.log("Exporting data...");
	}

	// Function Change Date Filter
	onChangeDateFilter(days: number) {
		this.selectDateFilter = `Last ${days} days`;
		// Lấy ngày hiện tại
		const dateTime = new Date();

		// Trừ đi số ngày nhận vào
		dateTime.setDate(dateTime.getDate() - days);

		// Định dạng ngày thành dd/MM/yyyy
		const formattedDate = dateTime
			.toLocaleDateString("vi-VN", {
				day: "2-digit",
				month: "2-digit",
				year: "numeric",
			})
			.toString();

		console.log(formattedDate); // Kết quả: dd/MM/yyyy
	}
}
