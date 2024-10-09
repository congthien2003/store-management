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
import { StoreService } from 'src/app/core/services/store/store.service';
import { Store } from 'src/app/core/models/interfaces/Store';
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
    private storeService: StoreService,
    private toastr: ToastrService
  ) {
    this.validateForm = this.fb.group({
      id: [0],
      name: [''],
      address: [''],
      phone: [''],
      idUser: [''],
    });
  }

  ngOnInit(): void {
    if (this.data.id != undefined) {
      this.formAdd = false;
      this.loadForm();
    }
  }
  loadForm(): void {
    this.storeService.getById(this.data.id).subscribe({
      next: (res) => {
        console.log(res.data);

        const store = res.data as Store;
        this.validateForm.setValue({
          id: store.id,
          name: store.name,
          address: store.address,
          phone: store.phone,
          idUser: store.idUser,
        });
      },
    });
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit(): void {
    const id = this.validateForm.value.id;
    console.log(id);
    console.log(this.validateForm.value);

    if (this.validateForm.valid) {
      const formValues = this.validateForm.value;
      this.storeService.update(this.validateForm.value, id).subscribe({
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
          this.toastr.error(err.message || 'Có lỗi xảy ra', 'Thất bại', {
            timeOut: 3000,
          });
          this.dialogRef.close(false);
        },
      });
    }
  }
}
