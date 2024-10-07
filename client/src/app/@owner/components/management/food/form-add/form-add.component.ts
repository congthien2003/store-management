import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  AbstractControl,
  FormGroup,
  FormsModule,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { MatButtonModule } from '@angular/material/button';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { FoodService } from 'src/app/core/services/food.service';
import { CategoryService } from 'src/app/core/services/category.service';
import { Pagination } from 'src/app/core/models/common/Pagination';
import { Category } from 'src/app/core/models/interfaces/Category';
const NzModule = [NzFormModule, NzSelectModule];
@Component({
  selector: 'app-form-add',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    ReactiveFormsModule,
    NzModule,
    FormsModule,
  ],
  templateUrl: './form-add.component.html',
  styleUrls: ['./form-add.component.scss'],
})
export class FormAddComponent {
  validateForm!: FormGroup;
  selectedValue: number = 0;
  idStore!: number;
  searchTerm: string = '';
  listCategory!: any[];
  selectedCategory!: number;
  pagi: Pagination = {
    totalPage: 0,
    totalRecords: 0,
    currentPage: 1,
    pageSize: 15,
    hasNextPage: false,
    hasPrevPage: false,
  };

  constructor(
    public dialogRef: MatDialogRef<FormAddComponent>,

    private fb: NonNullableFormBuilder,
    private FoodService: FoodService,
    private toastr: ToastrService,
    private categoryService: CategoryService
  ) {
    this.validateForm = this.fb.group({
      id: [0],
      name: ['', [Validators.required, Validators.minLength(4)]],
      price: [null, [Validators.required, Validators.min(0)]],
      quantity: [null, [Validators.required, Validators.min(1)]],
      status: [null, [Validators.required, Validators.pattern('true|false')]],
      idCategory: [null, [Validators.required]],
    });
  }
  ngOnInit(): void {
    console.log('form add');
    this.listCategories();
    console.log(this.listCategory);
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit(): void {
    const data = { ...this.validateForm.value };
    const payload = {
      ...data,
      status: data.status === 'true',
      price: Number(data.price),
      quantity: Number(data.quantity),
      idCategory: Number(data.idCategory),
    };
    this.FoodService.create(payload).subscribe({
      next: (res) => {
        console.log(res);
        if (res.isSuccess) {
          this.toastr.success(res.message, 'Thành công', {
            timeOut: 3000,
          });
          this.dialogRef.close(true);
        } else {
          this.toastr.error(res.message, 'Thất bại', {
            timeOut: 3000,
          });
          this.dialogRef.close(false);
        }
      },
      error: (err) => {
        this.toastr.error(err.message, 'Thất bại', {
          timeOut: 3000,
        });
        this.dialogRef.close(false);
      },
    });
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
          console.log(this.listCategory);

          this.pagi = res.data.pagination;
        },
        error: (err) => {
          console.log(err);
        },
      });
  }
}
