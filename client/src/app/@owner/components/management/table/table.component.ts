import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Pagination } from 'src/app/core/models/common/Pagination';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { TableService } from 'src/app/core/services/store/table.service';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';
import {
  FormGroup,
  NonNullableFormBuilder,
  Validators,
  FormsModule,
} from '@angular/forms';
import { ModalDeleteComponent } from 'src/app/shared/components/modal-delete/modal-delete.component';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { TablePagiComponent } from 'src/app/shared/components/table-pagi/table-pagi.component';

import { MatRadioModule } from '@angular/material/radio';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { NzButtonModule } from 'ng-zorro-antd/button';
const MatImport = [
  MatRadioModule,
  MatButtonModule,
  MatTableModule,
  MatDialogModule,
  MatTooltipModule,
  NzButtonModule,
];

const NzModule = [NzFormModule, NzSelectModule];
@Component({
  selector: 'app-table',
  standalone: true,
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
  imports: [
    CommonModule,
    MatImport,
    MatButtonModule,
    ReactiveFormsModule,
    PaginationComponent,
    TablePagiComponent,
    NzModule,
    FormsModule,
  ],
})
export class TableComponent implements OnInit {
  config = {
    displayedColumns: [
      {
        prop: 'id',
        display: 'STT',
      },
      {
        prop: 'status',
        display: 'Trạng thái',
      },
    ],
    hasAction: true,
  };
  pagi: Pagination = {
    totalPage: 0,
    totalRecords: 0,
    currentPage: 1,
    pageSize: 15,
    hasNextPage: false,
    hasPrevPage: false,
  };
  validateForm!: FormGroup;
  idStore!: number;
  listTable!: any[];

  private searchSubject = new Subject<string>();
  constructor(
    public dialog: MatDialog,
    private toastr: ToastrService,
    private tableService: TableService,
    private fb: NonNullableFormBuilder
  ) {
    this.searchSubject
      .pipe(debounceTime(1500), distinctUntilChanged())
      .subscribe((searchTerm) => {
        this.search(searchTerm);
      });
    this.validateForm = this.fb.group({
      id: [0],
      status: [],
      idStore: [null, [Validators.required]],
    });
  }
  ngOnInit(): void {
    this.loadlistTable();
  }

  loadlistTable(): void {
    this.idStore = JSON.parse(localStorage.getItem('idStore') ?? '');
    this.tableService.list(this.idStore, this.pagi).subscribe({
      next: (res) => {
        this.listTable = res.data.list;
        this.pagi = res.data.pagination;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
  onChangePage(currentPage: any): void {
    console.log(currentPage);
    this.pagi.currentPage = currentPage;
    this.loadlistTable();
  }
  search(searchTerm: string): void {
    this.loadlistTable();
    console.log(searchTerm);
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
    this.tableService.delete(id).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.toastr.success(res.message, 'Xóa thành công', {
            timeOut: 3000,
          });
          this.loadlistTable();
        } else {
          this.toastr.error(res.message, 'Xóa không thành công', {
            timeOut: 3000,
          });
        }
      },
      error: () => {
        this.toastr.error('', 'Có lỗi xảy ra', {
          timeOut: 3000,
        });
      },
    });
  }
  handCreate(): void {
    this.idStore = JSON.parse(localStorage.getItem('idStore') ?? '');
    if (this.idStore) {
      this.validateForm.patchValue({ idStore: this.idStore });
      this.validateForm.patchValue({ status: true });
    }
    console.log(this.validateForm.value);

    const data = { ...this.validateForm.value };

    this.tableService.create(data).subscribe({
      next: (res) => {
        console.log(res);
        if (res.isSuccess) {
          this.toastr.success(res.message, 'Thành công', {
            timeOut: 3000,
          });
          this.loadlistTable();
        } else {
          this.toastr.error(res.message, 'Thất bại', {
            timeOut: 3000,
          });
        }
      },
      error: (err) => {
        this.toastr.error(err.message, 'Thất bại', {
          timeOut: 3000,
        });
      },
    });
  }
  handUpdate(idTable: number, status: boolean): void {
    this.idStore = JSON.parse(localStorage.getItem('idStore') ?? '');
    if (this.idStore) {
      const newStatus = !status;
      this.validateForm.patchValue({ idStore: this.idStore });
      this.validateForm.patchValue({ status: newStatus });
    }
    this.tableService.update(this.validateForm.value, idTable).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.toastr.success(res.message, 'Thành công', {
            timeOut: 3000,
          });
          this.loadlistTable();
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
      },
    });
  }
}
