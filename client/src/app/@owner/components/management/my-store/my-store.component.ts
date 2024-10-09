import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { TablePagiComponent } from 'src/app/shared/components/table-pagi/table-pagi.component';
import { RolePipe } from 'src/app/core/utils/role.pipe';
import { SpinnerComponent } from 'src/app/shared/components/spinner/spinner.component';
import {
  FormGroup,
  NonNullableFormBuilder,
  Validators,
  FormsModule,
} from '@angular/forms';
import { Pagination } from 'src/app/core/models/common/Pagination';
import { ToastrService } from 'ngx-toastr';
import { StoreService } from 'src/app/core/services/store/store.service';
import { Store } from 'src/app/core/models/interfaces/Store';
import { UserService } from 'src/app/core/services/user/user.service';
import { ResolveStart } from '@angular/router';
import { FormEditComponent } from './form-edit/form-edit.component';

const MatImport = [
  MatRadioModule,
  MatButtonModule,
  MatTableModule,
  MatDialogModule,
  MatTooltipModule,
  NzButtonModule,
];

@Component({
  selector: 'app-my-store',
  standalone: true,
  templateUrl: './my-store.component.html',
  styleUrls: ['./my-store.component.scss'],
  imports: [
    CommonModule,
    MatImport,
    PaginationComponent,
    TablePagiComponent,
    SpinnerComponent,
    RolePipe,
    FormsModule,
  ],
})
export class MyStoreComponent implements OnInit {
  config = {
    displayedColumns: [
      {
        prop: 'name',
        display: 'Tên cửa hàng',
      },
      {
        prop: 'address',
        display: 'Địa chỉ',
      },
      {
        prop: 'phone',
        display: 'SDT',
      },
      {
        prop: 'idUser',
        display: 'Chủ sở hữu',
      },
    ],
    hasAction: true,
  };
  validateForm!: FormGroup;
  idStore!: number;
  nameUser!: string;
  myStore: {
    id?: number;
    name?: string;
    phone?: string;
    address?: string;
  } = {};
  constructor(
    public dialog: MatDialog,
    private toastr: ToastrService,
    private storeSevice: StoreService,
    private userService: UserService,
    private fb: NonNullableFormBuilder
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
    this.getMyStore();
  }
  getMyStore(): void {
    this.idStore = JSON.parse(localStorage.getItem('idStore') ?? '');
    this.storeSevice.getById(this.idStore).subscribe({
      next: (res) => {
        this.myStore = res.data;
        console.log(res.data);
        this.nameUser = res.data.userDTO.username;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  openEditDialog(): void {
    this.idStore = JSON.parse(localStorage.getItem('idStore') ?? '');
    const dialogRef = this.dialog.open(FormEditComponent, {
      data: { id: this.myStore.id },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.getMyStore();
      }
    });
  }
}
