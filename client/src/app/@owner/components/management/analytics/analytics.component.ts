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
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { DatetimePickerComponent } from "./datetime-picker/datetime-picker.component";
import { ExportService } from "src/app/core/services/store/export.service";
const MatImport = [
	MatDatepickerModule,
	MatInputModule,
	MatFormFieldModule,
	MatNativeDateModule,
	MatMenuModule,
	MatButtonModule,
	MatDialogModule,
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
	year = new Date().getFullYear();
	selectDateFilter: string = "Today only";

	constructor(
		private analystReportService: AnalystReportService,
		private exportExcel: ExportService,
		public dialog: MatDialog
	) {}
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

		this.loadData(formattedDate);
	}

	loadData(formattedDate: string) {
		this.analystReportService
			.getRevenueMonth(this.store.id, this.year)
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
		const dialogRef = this.dialog.open(DatetimePickerComponent, {});

		dialogRef.afterClosed().subscribe((result) => {
			if (result) {
				const startDate = new Date(result.start)
					.toLocaleDateString("vi-VN", {
						day: "2-digit",
						month: "2-digit",
						year: "numeric",
					})
					.toString();
				const endDate = new Date(result.end)
					.toLocaleDateString("vi-VN", {
						day: "2-digit",
						month: "2-digit",
						year: "numeric",
					})
					.toString();
				console.log(`Exporting data from ${startDate} to ${endDate}`);
				this.exportExcel
					.exportExcel(this.store.id, startDate, endDate)
					.subscribe({
						next: (res) => {
							const url = window.URL.createObjectURL(res);
							const a = document.createElement("a");
							a.href = url;
							a.download = "FoodSalesReport.xlsx"; // Đặt tên file
							document.body.appendChild(a);
							a.click();
							document.body.removeChild(a);
							window.URL.revokeObjectURL(url);
						},
					});
			}
		});
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
		this.loadData(formattedDate);
	}
}
