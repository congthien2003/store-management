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
import { CategoryService } from 'src/app/core/services/category.service';
import { Category } from 'src/app/core/models/interfaces/Category';
import { MatButtonModule } from '@angular/material/button';

import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';

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
  idStore?: number;
  constructor(
    public dialogRef: MatDialogRef<FormAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { idStore: number },
    private fb: NonNullableFormBuilder,
    private CategoryService: CategoryService,
    private toastr: ToastrService
  ) {
    this.idStore = Number(localStorage.getItem('idStore'));
    this.validateForm = this.fb.group({
      id: [0],
      name: ['', [Validators.required, Validators.minLength(4)]],
      idStore: [this.idStore, [Validators.required]],
    });
  }
  ngOnInit(): void {
    console.log('form add');
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit(): void {
    const data = { ...this.validateForm.value };

    this.CategoryService.create(data).subscribe({
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
}
