import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Pagination } from "src/app/core/models/interfaces/Pagination";
import { Store } from "src/app/core/models/interfaces/Store";

// Import Components
import { PaginationComponent } from "../../../../shared/components/pagination/pagination.component";
import { ModalDeleteComponent } from "src/app/shared/components/modal-delete/modal-delete.component";
import { FormAddComponent } from "../store/form-add/form-add.component";
import { FormEditComponent } from "../store/form-edit/form-edit.component";
import { FormsModule } from "@angular/forms";
import { SpinnerComponent } from "src/app/shared/components/spinner/spinner.component";
import { TablePagiComponent } from "src/app/shared/components/table-pagi/table-pagi.component";

// Import Mat
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { MatRadioModule } from "@angular/material/radio";
import { MatButtonModule } from "@angular/material/button";
import { MatTableModule } from "@angular/material/table";
import { MatTooltipModule } from "@angular/material/tooltip";
import { NzButtonModule } from "ng-zorro-antd/button";
import { debounceTime, distinctUntilChanged, Subject, timeout } from "rxjs";
import { StoreService } from "src/app/core/services/store/store.service";
import { User } from "src/app/core/models/interfaces/User";
import { UserService } from "src/app/core/services/user/user.service";
import { ApiResponse } from "src/app/core/models/interfaces/ApiResponse";

const MatImport = [
	MatRadioModule,
	MatButtonModule,	
	MatTableModule,
	MatDialogModule,
	MatTooltipModule,
	NzButtonModule,
];
@Component({
	selector: "app-store",
	standalone: true,
	templateUrl: "./store.component.html",
	styleUrls: ["./store.component.scss"],
	imports: [CommonModule,
			 PaginationComponent,
			 TablePagiComponent,
			 FormEditComponent,
			 FormAddComponent,
			 MatImport,
			 FormsModule,
			 SpinnerComponent,
			],
})
export class StoreComponent implements OnInit {
	config = {
		displayedColumns: [
			{
				prop: "id",
				display: "STT",
			},
			{
				prop: "name",
				display: "Tên cửa hàng",
			},
			{
				prop: "address",
				display: "Địa chỉ",
			},
			{
				prop: "phone",
				display: "Số điện thoại",
			},
			{
				prop: "idUser",
				display: "Tên người sở hữu",
			},
		],
		hasAction: true,
	};
	pagi: Pagination = {
		totalPage: 0,
		totalRecords: 0,
		currentPage: 1,
		pageSize: 10,
		hasNextPage: false,
		hasPrevPage: false,
	};
	searchTerm: string = "";
	listStore!: Store[];
	users: any[] = [];
	private searchSubject = new Subject<string>();
	constructor(public dialog: MatDialog, 
				private toastr: ToastrService,
				private storeService: StoreService,
				private userService:UserService
		) {
			this.searchSubject
			.pipe(
				debounceTime(1500), // Đợi 1.5 giây sau khi người dùng ngừng nhập
				distinctUntilChanged() // Chỉ phát khi giá trị khác với giá trị trước đó
			)
			.subscribe((searchTerm) => {
				this.search(searchTerm);
			});

	}
	

	ngOnInit(): void {
		this.loadListStore();
		this.loadUser();
	}

	loadListStore(): void {
		this.storeService.list(this.pagi,this.searchTerm).subscribe({
			next:(res) =>{
				this.listStore = res.data.list.map((store: any) => ({
					...store,
					User: store.userDTO  // Map userDTO to User
				}));
				this.pagi = res.data.pagination;
				
			},
			error:(err) =>{
				console.log(err);
			}
			
		})
	}
	loadUser(): void{
		this.userService.list(this.pagi,this.searchTerm).subscribe({
			next:(res) =>{
				this.users = res.data.list;
			},
			error:(err) =>{
				console.log(err);
			}
			
		})
	}

	onChangePage(currentPage: any): void {
		console.log(currentPage);
		this.pagi.currentPage = currentPage;
		this.loadUser();
		this.loadListStore();
	}

	openAddDialog(): void {
		const dialogRef = this.dialog.open(FormAddComponent);
		
		dialogRef.afterClosed().subscribe((result) => {
			if (result) {
				console.log("add form store");
				this.loadListStore();
			}
		});
	}

	openEditDialog(id: number): void {
		console.log("Editing store with ID:", id);
		const dialogRef = this.dialog.open(FormEditComponent, {
			data: { id: id },
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result) {
				this.loadListStore();
			}
		});
	}

	openDeleteDialog(id: number): void {
		const dialogRef = this.dialog.open(ModalDeleteComponent, {
			data: { id: id },
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result === true) {
				this.handleDelete(id);
			}
		});
	}

	handleDelete(id: number): void {
		this.storeService.deleteById(id).subscribe({
			next: (res) => {
				if (res.isSuccess) {
					this.toastr.success(res.message, "Xóa thành công", {
						timeOut: 3000,
					});
					this.loadListStore();
				} else {
					this.toastr.error(res.message, "Xóa không thành công", {
						timeOut: 3000,
					});
				}
			},
			error: () => {
				this.toastr.error("", "Có lỗi xảy ra", {
					timeOut: 3000,
				});
			},
		});
	}
	onSearchTerm(): void {
		this.searchSubject.next(this.searchTerm);
	}
	search(searchTerm: string): void {
		this.loadListStore();
		console.log(searchTerm);
	}
	getNameUser(idUser: number): string {
        const user = this.listStore.find((u) => u.User?.id === idUser); 
        return user?.User?.username || 'Không có tên';  
		
	}
}
