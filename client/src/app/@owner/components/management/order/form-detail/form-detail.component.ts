import {
  Component,
  EventEmitter,
  Inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  NonNullableFormBuilder,
  ValidatorFn,
  Validators,
  ReactiveFormsModule,
  FormsModule,
  MinLengthValidator,
  FormBuilder,
} from '@angular/forms';
import { Pagination } from 'src/app/core/models/common/Pagination';
import { OrderDetailService } from 'src/app/core/services/orderDetail.service';
import { OrderService } from 'src/app/core/services/order.service';
import { Order } from 'src/app/core/models/interfaces/Order';
import { FoodService } from 'src/app/core/services/food.service';
import { Food } from 'src/app/core/models/interfaces/Food';
import { ResolveStart } from '@angular/router';
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
  templateUrl: './form-detail.component.html',
  styleUrls: ['./form-detail.component.scss'],
})
export class FormDetailComponent implements OnInit {
  formAdd: boolean = true;
  validateForm!: FormGroup;
  total: { [id: number]: number | null } = {};
  listOrderDetail!: any[];
  food!: Food;
  listFood: any[] = [];
  pagi: Pagination = {
    totalPage: 0,
    totalRecords: 0,
    currentPage: 1,
    pageSize: 15,
    hasNextPage: false,
    hasPrevPage: false,
  };
  constructor(
    public dialogRef: MatDialogRef<FormDetailComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { id: number },
    private fb: NonNullableFormBuilder,
    private toastr: ToastrService,
    private orderDetailService: OrderDetailService,
    private orderService: OrderService,
    private foodService: FoodService
  ) {
    this.validateForm = this.fb.group({
      id: [this.data.id],
      nameUser: [''],
      phoneUser: [''],
      status: [null, [Validators.required]],
      total: [null, [Validators.required]],
      createdAt: [null, [Validators.required]],
      idTable: [null],
    });
  }
  ngOnInit(): void {
    if (this.data.id != undefined) {
      this.loadOrderDetail(this.data.id);
      this.formAdd = false;
      this.loadForm();
    }
  }

  loadForm(): void {
    this.orderService.getById(this.data.id).subscribe({
      next: (res) => {
        const order = res.data as Order;
        this.validateForm.setValue({
          id: order.id,
          nameUser: order.nameUser,
          phoneUser: order.phoneUser,
          total: order.total,
          createdAt: order.createdAt,
          status: order.status,
          idTable: order.idTable,
        });
      },
    });
  }
  onSubmit(): void {
    const newStatus = true;
    const id = this.validateForm.value.id;

    this.validateForm.patchValue({ status: newStatus });
    this.orderService.update(id, this.validateForm.value).subscribe({
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
    });
  }

  onNoClick(): void {
    this.dialogRef.close(false);
  }
  loadOrderDetail(id: number): void {
    if (id) {
      this.orderDetailService.list(id, this.pagi).subscribe({
        next: (res) => {
          this.listOrderDetail = res.data.list;
          this.pagi = res.data.pagination;
          this.listOrderDetail.forEach((detail) => {
            this.loadFood(detail.idFood, detail.quantity);
          });
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }
  loadFood(id: number, quantity: number): void {
    if (id) {
      this.foodService.getById(id).subscribe({
        next: (res) => {
          const foodWithQuantity = { ...res.data, quantity };
          this.listFood.push(foodWithQuantity);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }
}
