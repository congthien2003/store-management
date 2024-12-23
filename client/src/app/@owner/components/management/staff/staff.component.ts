import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { FormAddStaffComponent } from "./form-add-staff/form-add-staff.component";
import { UserService } from "src/app/core/services/user/user.service";
import { SpinnerComponent } from "src/app/shared/components/spinner/spinner.component";
import { PaginationComponent } from "src/app/shared/components/pagination/pagination.component";
import { StaffService } from "src/app/core/services/store/staff.service";
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { Staff } from "src/app/core/models/interfaces/Staff";
import { StaffInfoResponse } from "src/app/core/models/interfaces/Response/StaffInfoResponse";
import { MatButtonModule } from "@angular/material/button";
import { Subject, debounceTime, distinctUntilChanged } from "rxjs";

@Component({
	selector: "app-staff-management",
	standalone: true,
	imports: [
		CommonModule,
		FormsModule,
		SpinnerComponent,
		PaginationComponent,
		MatDialogModule,
		MatButtonModule,
	],
	templateUrl: "./staff.component.html",
	styleUrls: ["./staff.component.scss"],
})
export class StaffManagementComponent implements OnInit {
	staffList: StaffInfoResponse[] = [];
	searchTerm: string = "";
	storeId: number;

	config = {
		displayedColumns: [
			{ key: "index", display: "STT" },
			{ key: "name", display: "Tên nhân viên" },
			{ key: "email", display: "Email" },
			{ key: "phones", display: "Số điện thoại" },
			{ key: "age", display: "Tuổi" },
			{ key: "address", display: "Địa chỉ" },
		],
	};

	pagi = {
		totalPage: 0,
		totalRecords: 0,
		currentPage: 1,
		pageSize: 10,
		hasNextPage: false,
		hasPrevPage: false,
	};
	private searchSubject = new Subject<string>();
	constructor(
		private userService: UserService,
		private staffService: StaffService,
		private toastr: ToastrService,
		public dialog: MatDialog
	) {
		this.storeId = this.getStoreId();

		// Setup search debounce
		this.searchSubject
			.pipe(
				debounceTime(1500), // Wait 1.5 seconds after the last event
				distinctUntilChanged() // Only emit if value is different from previous
			)
			.subscribe(() => {
				this.pagi.currentPage = 1; // Reset to first page when searching
				this.loadStaffList();
			});
	}

	ngOnInit(): void {
		this.loadStaffList();
	}

	loadStaffList(): void {
		this.staffService
			.getByStore(
				this.storeId,
				this.pagi,
				this.searchTerm,
				false // Add filter parameter if needed
			)
			.subscribe({
				next: (response) => {
					if (response.isSuccess) {
						this.staffList = response.data.list;
						this.pagi = response.data.pagination;
					}
				},
				error: (error) => {
					this.toastr.error("Lỗi khi tải danh sách nhân viên");
				},
			});
	}

	onSearchTerm(): void {
		this.searchSubject.next(this.searchTerm);
	}

	onChangePage(page: number): void {
		this.pagi.currentPage = page;
		this.loadStaffList();
	}

	openAddDialog(): void {
		const dialogRef = this.dialog.open(FormAddStaffComponent, {
			width: "800px",
			data: { storeId: this.storeId },
		});

		dialogRef.afterClosed().subscribe((result) => {
			if (result) {
				this.loadStaffList();
			}
		});
	}

	openEditDialog(id: number): void {
		// Implement edit functionality
	}

	openDeleteDialog(id: number): void {
		if (confirm("Bạn có chắc chắn muốn xóa nhân viên này?")) {
			this.staffService.delete(id).subscribe({
				next: (response) => {
					if (response.isSuccess) {
						this.toastr.success("Xóa nhân viên thành công");
						this.loadStaffList();
					} else {
						this.toastr.error(response.message);
					}
				},
				error: (error) => {
					this.toastr.error("Lỗi khi xóa nhân viên");
				},
			});
		}
	}

	private getStoreId(): number {
		const storeInfo = sessionStorage.getItem("storeInfo");
		if (storeInfo) {
			return JSON.parse(storeInfo).id;
		}
		return 0;
	}
}
