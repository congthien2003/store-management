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
import { SpinnerComponent } from 'src/app/shared/components/spinner/spinner.component';
import { RolePipe } from 'src/app/core/utils/role.pipe';
import { Pagination } from 'src/app/core/models/common/Pagination';
import { Subject, debounceTime, distinctUntilChanged, timeout } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from 'src/app/core/services/store/order.service';
import { ModalDeleteComponent } from 'src/app/shared/components/modal-delete/modal-delete.component';
import {
  FormGroup,
  NonNullableFormBuilder,
  Validators,
  FormsModule,
} from '@angular/forms';
import { FormDetailComponent } from './form-detail/form-detail.component';
import { Order } from 'src/app/core/models/interfaces/Order';

const MatImport = [
  MatRadioModule,
  MatButtonModule,
  MatTableModule,
  MatDialogModule,
  MatTooltipModule,
  NzButtonModule,
];
@Component({
  selector: 'app-order',
  standalone: true,
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
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
export class OrderComponent implements OnInit {
  config = {
    displayedColumns: [
      {
        prop: 'id',
        display: 'STT',
      },
      {
        prop: 'idTable',
        display: 'Bàn',
      },
      {
        prop: 'total',
        display: 'Số tiền',
      },
      {
        prop: 'createdAt',
        display: 'Thời gian bắt đầu',
      },
      {
        prop: 'finishedAt',
        display: 'Thời gian kết thúc',
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
    pageSize: 10,
    hasNextPage: false,
    hasPrevPage: false,
  };
  validateForm!: FormGroup;
  total: { [id: number]: number | null } = {};
  idStore!: number;
  private searchSubject = new Subject<string>();
  searchTerm: string = '';
  listOrder!: any[];
  constructor(
    public dialog: MatDialog,
    private toastr: ToastrService,
    private orderService: OrderService,
    private fb: NonNullableFormBuilder
  ) {
    this.searchSubject
      .pipe(debounceTime(1500), distinctUntilChanged())
      .subscribe((searchTerm) => {
        this.search(searchTerm);
      });
    this.validateForm = this.fb.group({
      id: [0],
      status: [null, [Validators.required]],
      total: [null, [Validators.required]],
      createdAt: [null, [Validators.required]],
      finishedAt: [null],
      idTable: [null],
    });
  }

  ngOnInit(): void {
    this.loadListOrder();
  }

  loadListOrder(): void {
    this.idStore = JSON.parse(localStorage.getItem('idStore') ?? '');
    this.orderService.list(this.idStore, this.pagi, this.searchTerm).subscribe({
      next: (res) => {
        this.listOrder = res.data.list;
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
    this.loadListOrder();
  }
  search(searchTerm: string): void {
    this.loadListOrder();
    console.log(searchTerm);
  }
  onSearchTerm(): void {
    this.searchSubject.next(this.searchTerm);
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
    this.orderService.delete(id).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.toastr.success(res.message, 'Xóa thành công', {
            timeOut: 3000,
          });
          this.loadListOrder();
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
  openDetailDialog(id: number): void {
    const dialogRef = this.dialog.open(FormDetailComponent, {
      data: { id: id },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadListOrder();
      }
    });
  }
  // calculate(id: number): void {
  //   this.orderService.calculateTotal(id).subscribe({
  //     next: (res) => {
  //       if (res.isSuccess) {
  //         this.total[id] = res.data;
  //         this.orderService.getById(id).subscribe({
  //           next: (res) => {
  //             const order = res.data as Order;
  //             this.validateForm.setValue({
  //               id: order.id,

  //               total: this.total[id],
  //               createdAt: order.createdAt,
  //               status: order.status,
  //               idTable: res.data.tableDTO.id,
  //             });
  //             this.orderService.update(id, this.validateForm.value).subscribe({
  //               next: (res) => {},
  //             });
  //           },
  //         });
  //       }
  //     },
  //     error: (err) => {
  //       console.log(err);
  //     },
  //   });
  // }
  calculateTotalPrice(items: { idFood: number; price: number }[]): number {
    return items.reduce((total, item) => total + item.price, 0);
  }
}
