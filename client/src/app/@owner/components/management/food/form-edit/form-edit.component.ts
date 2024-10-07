import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import {
  FormGroup,
  NonNullableFormBuilder,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { FoodService } from 'src/app/core/services/food.service';
import { Food } from 'src/app/core/models/interfaces/Food';
import { CategoryService } from 'src/app/core/services/category.service';
import { Pagination } from 'src/app/core/models/common/Pagination';
const NzModule = [NzFormModule, NzSelectModule];

@Component({
  selector: 'app-form-edit',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    ReactiveFormsModule,
    NzModule,
    FormsModule,
  ],
  templateUrl: './form-edit.component.html',
  styleUrls: ['./form-edit.component.scss'],
})
export class FormEditComponent implements OnInit {
  formAdd: boolean = true;
  validateForm!: FormGroup;
  selectedValue: number = 0;
  idStore!: number;
  selectedCategory!: number;
  listCategory!: any[];
  searchTerm: string = '';

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
    private categoryService: CategoryService
  ) {
    this.validateForm = this.fb.group({
      id: [this.data.id],
      name: ['', [Validators.required, Validators.minLength(4)]],
      price: [null, [Validators.required, Validators.min(0)]],
      quantity: [null, [Validators.required, Validators.min(1)]],
      status: [null, [Validators.required, Validators.pattern('true|false')]],
      idCategory: [null, [Validators.required]],
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
        this.validateForm.setValue({
          id: food.id,
          name: food.name,
          price: food.price,
          quantity: food.quantity,
          status: food.status,
          idCategory: food.idCategory,
        });
      },
    });
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit(): void {
    if (this.validateForm.valid) {
      const formValues = this.validateForm.value;

      const payload = {
        ...formValues,
        status: formValues.status === 'true',
        price: Number(formValues.price),
        quantity: Number(formValues.quantity),
      };
      const id = formValues.id;

      this.foodService.update(payload, id).subscribe({
        next: (res) => {
          if (res.isSuccess) {
            this.toastr.success(res.message, 'Thành công', {
              timeOut: 3000,
            });

            this.dialogRef.close(true);
          } else {
            this.toastr.error(res.message, 'Thất bại', {
              timeOut: 3000,
            });
          }
        },
        error: (err) => {
          console.error(err);
          this.toastr.error(err.message || 'Có lỗi xảy ra', 'Thất bại', {
            timeOut: 3000,
          });
          this.dialogRef.close(false);
        },
      });
    } else {
      this.toastr.error('Vui lòng kiểm tra lại thông tin', 'Thất bại', {
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
    this.idStore = JSON.parse(localStorage.getItem('idStore') ?? '');
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
