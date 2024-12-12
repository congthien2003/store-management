import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AiPredictService } from "src/app/core/services/store/ai-predict.service";
import { ComboService } from "src/app/core/services/store/combo.service";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { StoreService } from "src/app/core/services/store/store.service";
import { Store } from "src/app/core/models/interfaces/Store";
import { FormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatDialogModule, MatDialog } from "@angular/material/dialog";
import { MatRadioModule } from "@angular/material/radio";
import { MatTableModule } from "@angular/material/table";
import { MatTooltipModule } from "@angular/material/tooltip";
import { NzButtonModule } from "ng-zorro-antd/button";
import { ToastrService } from "ngx-toastr";
import { Subject, debounceTime, distinctUntilChanged } from "rxjs";
import { Category } from "src/app/core/models/interfaces/Category";
import { Pagination } from "src/app/core/models/interfaces/Common/Pagination";
import { Food } from "src/app/core/models/interfaces/Food";
import { FirebaseService } from "src/app/core/services/firebase.service";
import { LoaderService } from "src/app/core/services/loader.service";
import { CategoryService } from "src/app/core/services/store/category.service";
import { FoodService } from "src/app/core/services/store/food.service";
import { CategoryPipe } from "src/app/core/utils/category.pipe";
import { PricePipe } from "src/app/core/utils/price.pipe";
import { PaginationComponent } from "src/app/shared/components/pagination/pagination.component";
import { SpinnerComponent } from "src/app/shared/components/spinner/spinner.component";
import { FormAddComponent } from "../category/form-add/form-add.component";
import { FormEditComponent } from "../category/form-edit/form-edit.component";
import { ModalDeleteComponent } from "src/app/shared/components/modal-delete/modal-delete.component";
import { Combo } from "src/app/core/models/interfaces/Combo";
import { ButtonAiComponent } from "src/app/shared/components/button-ai/button-ai.component";
import { ViewDataComponent } from "./view-data/view-data.component";

const MatImport = [
	MatRadioModule,
	MatButtonModule,
	MatTableModule,
	MatDialogModule,
	MatTooltipModule,
	NzButtonModule,
];

@Component({
	selector: "app-combo",
	standalone: true,
	imports: [
		CommonModule,
		MatImport,
		PaginationComponent,
		FormEditComponent,
		FormAddComponent,
		SpinnerComponent,
		FormsModule,
		CategoryPipe,
		PricePipe,
		ButtonAiComponent,
	],
	templateUrl: "./combo.component.html",
	styleUrls: ["./combo.component.scss"],
})
export class ComboComponent {
	store!: Store;
	idUser!: number;
	titleButtonAi: string = "Xem";
	config = {
		displayedColumns: [
			{
				prop: "id",
				display: "STT",
			},
			// {
			// 	prop: "images",
			// 	display: "Hình ảnh",
			// },
			{
				prop: "name",
				display: "Tên",
			},
			{
				prop: "price",
				display: "Giá",
			},
			{
				prop: "description",
				display: "Mô tả",
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
	listCombo!: Combo[];
	listCategory!: Category[];

	private searchSubject = new Subject<string>();
	constructor(
		public dialog: MatDialog,
		private toastr: ToastrService,
		private foodService: FoodService,
		private categoryService: CategoryService,
		private firebaseSerivce: FirebaseService,
		private loader: LoaderService,
		private comboService: ComboService,
		private AIService: AiPredictService
	) {
		// this.searchSubject
		// 	.pipe(debounceTime(1500), distinctUntilChanged())
		// 	.subscribe((searchTerm) => {
		// 		this.search(searchTerm);
		// 	});
	}
	ngOnInit(): void {
		this.store = JSON.parse(
			sessionStorage.getItem("storeInfo") ?? ""
		) as Store;
		this.loadListCombos();
	}

	viewFromAI() {
		this.AIService.getPopularCombos(this.store.id).subscribe((data) => {
			console.log("AI Prediction:", data);
			const dialogRef = this.dialog.open(ViewDataComponent, {
				data: {
					listCombo: data,
				},
			});

			dialogRef.afterClosed().subscribe((result) => {});
		});
	}

	loadListCombos() {
		this.comboService
			.getAllByStore(this.store.id, this.pagi, "", "")
			.subscribe((res) => {
				console.log(res);
				this.listCombo = res.data.list;
			});
	}

	onChangePage(currentPage: any): void {
		this.pagi.currentPage = currentPage;
		// Function Load
	}

	openAddDialog(): void {
		const dialogRef = this.dialog.open(FormAddComponent, {
			data: { idStore: this.store.id },
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result != null) {
				// Function Load
			}
		});
	}

	openEditDialog(id: number): void {
		const dialogRef = this.dialog.open(FormEditComponent, {
			data: {
				id: id,
				listCategory: this.listCategory,
			},
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result) {
				// Function Load
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

	async handleDelete(id: number): Promise<void> {
		try {
			const food = this.listCombo.find((e) => {
				return e.id == id;
			});
			if (food) {
				// await this.firebaseSerivce.deleteFileFromFirebase(
				// 	food?.imageUrl
				// );
				console.log("Xóa ảnh thành công !");
			}
		} catch (error) {
			console.log(error);
		}
		this.foodService.deleteById(id).subscribe({
			next: (res) => {
				if (res.isSuccess) {
					this.toastr.success(res.message, "Xóa thành công", {
						timeOut: 3000,
					});
					// Function Load
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
		// Function Load
		console.log(searchTerm);
	}
}
