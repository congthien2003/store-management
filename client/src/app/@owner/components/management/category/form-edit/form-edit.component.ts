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
import { Category } from 'src/app/core/models/interfaces/Category';
import { CategoryService } from 'src/app/core/services/category.service';
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

  constructor(
    public dialogRef: MatDialogRef<FormEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { id: number },
    private fb: NonNullableFormBuilder,
    private categoryService: CategoryService,
    private toastr: ToastrService
  ) {
    this.validateForm = this.fb.group({
      id: [this.data.id],
      name: ['', [Validators.required, Validators.minLength(4)]],
    });
  }
  ngOnInit(): void {
    if (this.data.id != undefined) {
      this.formAdd = false;
      console.log('Data', this.data.id);
      this.loadForm();
    }
  }
  loadForm(): void {
    this.categoryService.getById(this.data.id).subscribe({
      next: (res) => {
        const category = res.data as Category;
        this.validateForm.setValue({
          id: category.id,
          name: category.name,
        });
      },
    });
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit(): void {
    const id = this.validateForm.get('id')?.value;
    this.categoryService.update(this.validateForm.value, id).subscribe({
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
        console.log(err);
        this.toastr.error(err.message.message, 'Thất bại', {
          timeOut: 3000,
        });
        this.dialogRef.close(false);
      },
    });
  }
}
