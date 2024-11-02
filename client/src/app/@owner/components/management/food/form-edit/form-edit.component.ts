import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzSelectModule } from "ng-zorro-antd/select";
import { MatButtonModule } from "@angular/material/button";
import { CommonModule } from "@angular/common";
import {
	FormGroup,
	NonNullableFormBuilder,
	Validators,
	ReactiveFormsModule,
	FormsModule,
} from "@angular/forms";
import { FoodService } from "src/app/core/services/store/food.service";
import { Food } from "src/app/core/models/interfaces/Food";
import { CategoryService } from "src/app/core/services/store/category.service";
import { Pagination } from "src/app/core/models/common/Pagination";
import { FirebaseService } from "src/app/core/services/api-third/firebase.service";
const NzModule = [NzFormModule, NzSelectModule];

@Component({
	selector: "app-form-edit",
	standalone: true,
	imports: [
		CommonModule,
		MatButtonModule,
		ReactiveFormsModule,
		NzModule,
		FormsModule,
	],
	templateUrl: "./form-edit.component.html",
	styleUrls: ["./form-edit.component.scss"],
})
export class FormEditComponent implements OnInit {
	formAdd: boolean = true;
	validateForm!: FormGroup;
	selectedValue: number = 0;
	idStore!: number;
	selectedCategory!: number;
	listCategory!: any[];
	searchTerm: string = "";
	selectedFile: File | null = null;
	imageUrl: string | null = null;
	pagi: Pagination = {
		totalPage: 0,
		totalRecords: 0,
		currentPage: 1,
		pageSize: 15,
		hasNextPage: false,
		hasPrevPage: false,
	};

	constructor(
		public dialogRef: MatDialogRef<FormEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: { id: number },
		private fb: NonNullableFormBuilder,
		private foodService: FoodService,
		private toastr: ToastrService,
		private categoryService: CategoryService,
		private firebaseService: FirebaseService
	) {
		this.validateForm = this.fb.group({
			id: [this.data.id],
			name: ["", [Validators.required, Validators.minLength(4)]],
			price: [null, [Validators.required, Validators.min(0)]],
			quantity: [null, [Validators.required, Validators.min(1)]],
			status: [
				null,
				[Validators.required, Validators.pattern("true|false")],
			],
			idCategory: [null, [Validators.required]],
			imageUrl: [null],
		});
	}
	ngOnInit(): void {
		if (this.data.id != undefined) {
			this.formAdd = false;
			this.loadForm();
			this.listCategories();
		}
	}
	loadForm(): void {
		this.foodService.getById(this.data.id).subscribe({
			next: (res) => {
				const food = res.data as Food;
				this.imageUrl = food.imageUrl;
				this.validateForm.setValue({
					id: food.id,
					name: food.name,
					price: food.price,
					quantity: food.quantity,
					status: food.status,
					idCategory: res.data.categoryDTO.id,
					imageUrl: food.imageUrl,
				});
				console.log(res.data);
			},
		});
	}
	onNoClick(): void {
		this.dialogRef.close(false);
	}
	onFileSelected(event: any): void {
		this.selectedFile = event.target.files[0];
    if (this.selectedFile) {
        const reader = new FileReader();
        reader.onload = (e: any) => {
            this.imageUrl = e.target.result; 
        };
        reader.readAsDataURL(this.selectedFile);
    }
	}
	async onSubmit(): Promise<void> {
		if (this.validateForm.valid) {
			const formValues = this.validateForm.value;
			console.log(this.selectedFile);

			const payload = {
				...formValues,
				status: formValues.status === "true",
				price: Number(formValues.price),
				quantity: Number(formValues.quantity),
				imageUrl: this.selectedFile
                ? await this.firebaseService.saveFile(`foods/${this.selectedFile.name}`, this.selectedFile)
                : this.imageUrl, 
        };
        const id = formValues.id;

			this.foodService.update(payload, id).subscribe({
				next: (res) => {
					if (res.isSuccess) {
						this.toastr.success(res.message, "Thành công", {
							timeOut: 3000,
						});

						this.dialogRef.close(true);
					} else {
						this.toastr.error(res.message, "Thất bại", {
							timeOut: 3000,
						});
					}
				},
				error: (err) => {
					console.error(err);
					this.toastr.error(
						err.message || "Có lỗi xảy ra",
						"Thất bại",
						{
							timeOut: 3000,
						}
					);
					this.dialogRef.close(false);
				},
			});
		} else {
			this.toastr.error("Vui lòng kiểm tra lại thông tin", "Thất bại", {
				timeOut: 3000,
			});
		}
	}

	onCategoryChange(categoryId: number): void {
		this.selectedCategory = categoryId;
		this.validateForm.patchValue({
			idCategory: categoryId,
		});
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
}
