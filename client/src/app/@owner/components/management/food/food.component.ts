import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Pagination } from "src/app/core/models/common/Pagination";
import { MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { FormAddComponent } from "./form-add/form-add.component";
import { FoodService } from "src/app/core/services/store/food.service";
import { debounce, debounceTime, distinctUntilChanged, Subject } from "rxjs";
import { FormEditComponent } from "./form-edit/form-edit.component";
import { ModalDeleteComponent } from "src/app/shared/components/modal-delete/modal-delete.component";
import { MatRadioModule } from "@angular/material/radio";
import { MatButtonModule } from "@angular/material/button";
import { MatTableModule } from "@angular/material/table";
import { MatDialogModule } from "@angular/material/dialog";
import { MatTooltipModule } from "@angular/material/tooltip";
import { NzButtonModule } from "ng-zorro-antd/button";
import { PaginationComponent } from "src/app/shared/components/pagination/pagination.component";
import { TablePagiComponent } from "src/app/shared/components/table-pagi/table-pagi.component";
import { SpinnerComponent } from "src/app/shared/components/spinner/spinner.component";
import { RolePipe } from "src/app/core/utils/role.pipe";
import { FormsModule } from "@angular/forms";
import { CategoryService } from "src/app/core/services/store/category.service";
import { FirebaseService } from "src/app/core/services/api-third/firebase.service";
const MatImport = [
	MatRadioModule,
	MatButtonModule,
	MatTableModule,
	MatDialogModule,
	MatTooltipModule,
	NzButtonModule,
];

@Component({
	selector: "app-food",
	standalone: true,
	templateUrl: "./food.component.html",
	styleUrls: ["./food.component.scss"],
	imports: [
		CommonModule,
		MatImport,
		PaginationComponent,
		TablePagiComponent,
		FormEditComponent,
		FormAddComponent,
		SpinnerComponent,
		RolePipe,
		FormsModule,
	],
})
export class FoodComponent implements OnInit {
	config = {
		displayedColumns: [
			{
				prop: "id",
				display: "STT",
			},
			{
				prop: "name",
				display: "Tên món ăn",
			},
			{
				prop: "price",
				display: "Giá tiền",
			},
			{
				prop: "quantity",
				display: "Số lượng",
			},
			{
				prop: "status",
				display: "Trạng thái",
			},
			{
				prop: "idCategory",
				display: "Thể loại",
			},
			{
				prop: "imageUrl",
				display: "Hình ảnh",
			},
			{
				display: "Hành động",
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
	searchTerm: string = "";
	listFood!: any[];
	listCategory!: any[];
	idCategory!: number;
	idStore!: number;
	private searchSubject = new Subject<string>();

	constructor(
		public dialog: MatDialog,
		private toastr: ToastrService,
		private foodService: FoodService,
		private categoryService: CategoryService,
		private firebaseService: FirebaseService
	) {
		this.searchSubject
			.pipe(debounceTime(1500), distinctUntilChanged())
			.subscribe((searchTerm) => {
				this.search(searchTerm);
			});
	}
	ngOnInit(): void {
		this.loadlistFood();
		this.listCategories();
	}
	loadlistFood(): void {
		this.idStore = JSON.parse(localStorage.getItem("idStore") ?? "");
		this.foodService
			.getByIdStore(this.idStore, this.pagi, this.searchTerm)
			.subscribe({
				next: (res) => {
					this.listFood = res.data.list;
					this.pagi = res.data.pagination;
					for (const food of this.listFood) {
						const imageRef =
							this.firebaseService.getImageRefFromUrl(
								food.imageUrl
							);
					}
				},
				error: (err) => {
					console.log(err);
				},
			});
	}
	onChangePage(currentPage: any): void {
		this.pagi.currentPage = currentPage;
		this.loadlistFood();
	}
	openAddDialog(): void {
		const dialogRef = this.dialog.open(FormAddComponent, {
			data: { idStore: this.idStore },
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result) {
				this.loadlistFood();
			}
		});
	}
	openEditDialog(id: number): void {
		const dialogRef = this.dialog.open(FormEditComponent, {
			data: { id: id },
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result) {
				this.loadlistFood();
			}
		});
	}
	openDeleteDialog(id: number): void {
		const dialogRef = this.dialog.open(ModalDeleteComponent, {
			data: { id: id },
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result === true) {
				this.handDelete(id);
			}
		});
	}
	handDelete(id: number): void {
		this.foodService.delete(id).subscribe({
			next: (res) => {
				if (res.isSuccess) {
					this.toastr.success(res.message, "Xóa thành công", {
						timeOut: 3000,
					});
					this.loadlistFood();
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
		this.loadlistFood();
		console.log(searchTerm);
	}

	listCategories(): void {
		this.idStore = JSON.parse(localStorage.getItem("idStore") ?? "");
		this.categoryService
			.list(this.idStore, this.pagi, this.searchTerm)
			.subscribe({
				next: (res) => {
					this.listCategory = res.data.list;
					this.pagi = res.data.pagination;
				},
				error: (err) => {
					console.log(err);
				},
			});
	}

	getNameCategory(idCategory: number): string {
		if (!this.listCategory) {
			return "Unknown";
		}
		const category = this.listCategory.find((x) => x.id === idCategory);
		return category ? category.name : "Unknown";
	}
}
