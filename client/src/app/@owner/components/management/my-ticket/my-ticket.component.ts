import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Pagination } from "src/app/core/models/interfaces/Common/Pagination";
import { FormAddComponent } from "./form-add/form-add.component";
import { PaginationComponent } from "../../../../shared/components/pagination/pagination.component";
import { ModalDeleteComponent } from "src/app/shared/components/modal-delete/modal-delete.component";
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { TicketService } from "src/app/core/services/ticket.service";
import { MatMenuModule } from "@angular/material/menu";
import { NzButtonModule } from "ng-zorro-antd/button";
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatTableModule } from "@angular/material/table";
import { MatButtonModule } from "@angular/material/button";
import { MatRadioModule } from "@angular/material/radio";
import { Store } from "src/app/core/models/interfaces/Store";
import { UserService } from "src/app/core/services/user/user.service";
import { debounceTime, distinctUntilChanged, Subject, timeout } from "rxjs";
const MatImport = [
	MatRadioModule,
	MatButtonModule,
	MatTableModule,
	MatDialogModule,
	MatTooltipModule,
	NzButtonModule,
	MatMenuModule,
];
@Component({
	selector: "app-my-ticket",
	standalone: true,
	templateUrl: "./my-ticket.component.html",
	styleUrls: ["./my-ticket.component.scss"],
	imports: [CommonModule, PaginationComponent, MatImport, FormAddComponent],
})
export class MyTicketComponent implements OnInit {
	config = {
		displayedColumns: [
			{
				prop: "id",
				display: "STT",
			},
			{
				prop: "username",
				display: "Tên người dùng",
			},
			{
				prop: "title",
				display: "Tiêu đề",
			},
			{
				prop: "CreatedAt",
				display: "Thời gian",
			},
			{
				prop: "Status",
				display: "Trạng thái",
			},
		],
		hasAction: true,
	};
	pagi: Pagination = {
		totalPage: 0,
		totalRecords: 0,
		currentPage: 1,
		pageSize: 5,
		hasNextPage: false,
		hasPrevPage: false,
	};
	listTicket!: any[];
	store!: Store;
	private searchSubject = new Subject<string>();
	searchTerm: string = "";
	status: number = 0;
	nameuser: string = "";
	nameUserCache = new Map<number, string>();
	constructor(
		public dialog: MatDialog,
		private toastr: ToastrService,
		private ticketService: TicketService,
		private userService: UserService
	) {}
	ngOnInit(): void {
		this.store = JSON.parse(
			sessionStorage.getItem("storeInfo") ?? ""
		) as Store;
		this.loadListTicket(this.status);
	}
	loadListTicket(status: number): void {
		this.ticketService
			.getMyTicket(this.store.idUser, status, this.pagi)
			.subscribe({
				next: (res) => {
					this.listTicket = res.data.list;
					this.pagi = res.data.pagination;
				},
				error: (err) => {
					console.log(err);
				},
			});
	}
	onChangePage(currentPage: any): void {
		this.pagi.currentPage = currentPage;
		console.log(currentPage);
		this.loadListTicket(this.status);
	}
	onSearchTerm(): void {
		this.searchSubject.next(this.searchTerm);
	}

	search(searchTerm: string): void {
		this.loadListTicket(this.status);
		console.log(searchTerm);
	}
	getNameUser(id: number): string {
		if (this.nameUserCache.has(id)) {
			return this.nameUserCache.get(id) || "Unknown User";
		}

		this.userService.getById(id).subscribe({
			next: (res) => {
				this.nameUserCache.set(id, res.data.username);
			},
			error: (err) => {
				console.log(err);
				this.nameUserCache.set(id, "Unknown User");
			},
		});

		return "Loading...";
	}
	openAddDialog() {
		const dialogRef = this.dialog.open(FormAddComponent, {
			data: {
				idUser: this.store.idUser,
				email: this.store,
			},
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result) {
				this.loadListTicket(this.status);
			}
		});
	}

	convertToFormattedDate(dateTime: Date): string {
		return dateTime.toLocaleString("vi-VN", {
			day: "2-digit",
			month: "2-digit",
			year: "numeric",
		});
	}
}
