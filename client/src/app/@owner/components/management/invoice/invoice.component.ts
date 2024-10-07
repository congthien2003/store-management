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
import { ToastrService } from 'ngx-toastr';
import { InvoiceService } from 'src/app/core/services/invoice.service';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';
const MatImport = [
  MatRadioModule,
  MatButtonModule,
  MatTableModule,
  MatDialogModule,
  MatTooltipModule,
  NzButtonModule,
];
import {
  FormGroup,
  NonNullableFormBuilder,
  Validators,
  FormsModule,
} from '@angular/forms';
import { Invoice } from 'src/app/core/models/interfaces/Invoice';
import { PaymentTypeService } from 'src/app/core/services/paymentType.service';
import { PaymentType } from 'src/app/core/models/interfaces/PaymentType';
@Component({
  selector: 'app-invoice',
  standalone: true,
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.scss'],
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
export class InvoiceComponent implements OnInit {
  config = {
    displayedColumns: [
      {
        prop: 'id',
        display: 'STT',
      },
      {
        prop: 'total',
        display: 'Tổng tiền',
      },
      {
        prop: 'createdAt',
        display: 'Bắt đầu',
      },
      {
        prop: 'finishedAt',
        display: 'Kết thúc',
      },
      {
        prop: 'idOrder',
        display: 'Order',
      },
      {
        prop: 'idPaymentType',
        display: 'Thanh toán',
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
  searchTerm: string = '';
  idStore!: number;
  listInvoice!: any[];
  listPayment: { id: number; name: string }[] = [];
  validateForm!: FormGroup;
  private searchSubject = new Subject<string>();
  constructor(
    public dialog: MatDialog,
    private toastr: ToastrService,
    private invoiceService: InvoiceService,
    private fb: NonNullableFormBuilder,
    private paymentTypeService: PaymentTypeService
  ) {
    this.searchSubject
      .pipe(debounceTime(1500), distinctUntilChanged())
      .subscribe((searchTerm) => {
        this.search(searchTerm);
      });
    this.validateForm = fb.group({
      id: [0],
      status: [],
      idOrder: [],
      createdAt: [],
      finishedAt: [],
      totalOrder: [],
      idPaymentType: [],
    });
  }
  ngOnInit(): void {
    this.loadListInvoice();
    this.loadListPayment();
  }

  loadListInvoice(): void {
    this.idStore = JSON.parse(localStorage.getItem('idStore') ?? '');

    this.invoiceService
      .list(this.idStore, this.pagi, this.searchTerm)
      .subscribe({
        next: (res) => {
          this.listInvoice = res.data.list;
          this.pagi = res.data.pagination;
        },
        error: (err) => {
          console.log(err);
        },
      });
  }
  search(searchTerm: string): void {
    this.loadListInvoice();
    console.log(searchTerm);
  }
  onSearchTerm(): void {
    this.searchSubject.next(this.searchTerm);
  }
  onChangePage(currentPage: any): void {
    console.log(currentPage);
    this.pagi.currentPage = currentPage;
    this.loadListInvoice();
  }
  handUpdate(idOrder: number, status: boolean): void {
    const newStatus = !status;
    this.validateForm.patchValue({ status: newStatus });
    this.invoiceService.getById(idOrder).subscribe({
      next: (res) => {
        const invoice = res.data as Invoice;
        this.validateForm.setValue({
          id: invoice.id,
          status: newStatus,
          idOrder: invoice.idOrder,
          createdAt: invoice.createdAt,
          finishedAt: invoice.finishedAt,
          totalOrder: invoice.totalOrder,
          idPaymentType: invoice.idPaymentType,
        });
        this.invoiceService.update(idOrder, this.validateForm.value).subscribe({
          next: (res) => {
            if (res.isSuccess) {
              this.toastr.success(res.message, 'Thành công', {
                timeOut: 3000,
              });
              this.loadListInvoice();
            } else {
              this.toastr.error(res.message, 'Cập nhật thất bại', {
                timeOut: 3000,
              });
            }
          },
          error: (err) => {
            this.toastr.error(err.message || 'Có lỗi xảy ra', 'Thất bại', {
              timeOut: 3000,
            });
          },
        });
      },
    });
  }
  loadListPayment(): void {
    this.idStore = JSON.parse(localStorage.getItem('idStore') ?? '');
    this.paymentTypeService
      .list(this.idStore, this.pagi, this.searchTerm)
      .subscribe({
        next: (res) => {
          this.listPayment = res.data.list;
          this.pagi = res.data.pagination;
        },
        error: (err) => {
          console.log(err);
        },
      });
  }
  getNamePayment(id: number): string {
    if (!this.listPayment) {
      return 'Unknown';
    }
    const payment = this.listPayment.find((x) => x.id === id);
    return payment ? payment.name : 'Unknown';
  }
}
