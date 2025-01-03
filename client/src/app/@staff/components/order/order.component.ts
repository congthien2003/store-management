import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { MatMenuModule } from '@angular/material/menu';
import { FormsModule } from '@angular/forms';
import { SpinnerComponent } from 'src/app/shared/components/spinner/spinner.component';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { Pagination } from 'src/app/core/models/interfaces/Common/Pagination';
import { Store } from 'src/app/core/models/interfaces/Store';
import { OrderResponse } from 'src/app/core/models/interfaces/Response/OrderResponse';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from 'src/app/core/services/store/order.service';
import { ApiResponse } from 'src/app/core/models/interfaces/Common/ApiResponse';
import { FormEditComponent } from './form-edit/form-edit.component';


const MatImport = [
	MatRadioModule,
	MatButtonModule,
	MatTableModule,
	MatDialogModule,
	MatTooltipModule,
	NzButtonModule,
	MatMenuModule,
];
@Component({
  selector: 'app-order',
  standalone: true,
  imports: [
    CommonModule,
		MatImport,
		PaginationComponent,
		SpinnerComponent,
		FormsModule
  ],
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
    config = {
      displayedColumns: [
        {
          prop: "id",
          display: "STT",
        },
        {
          prop: "idTable",
          display: "Số bàn",
        },
        {
          prop: "total",
          display: "Giá tiền",
        },
        {
          prop: "status",
          display: "Trạng thái",
        },
      ],
      hasAction: true,
    };
    pagi: Pagination = {
      totalPage: 0,
      totalRecords: 0,
      currentPage: 1,
      pageSize: 9,
      hasNextPage: false,
      hasPrevPage: false,
    };
    selectedValueStatus: number = 0;
	  filter: boolean = false;
	  status: boolean = false;

	  listOrder: OrderResponse[] = [];

	  store!: Store;

    headerTitle: string = "Quản lý đơn đặt hàng";
    headerClass: string = "header-default";

    constructor(
      public dialog: MatDialog,
      private toastr: ToastrService,
      private orderService: OrderService
    ) {}
    ngOnInit(): void {
      this.store = JSON.parse(
        sessionStorage.getItem("storeInfo") ?? ""
      ) as Store;
      this.loadListOrder();
    }
    handleResponse(res: ApiResponse) {
      if (res.isSuccess) {
        this.toastr.success(res.message, "Thành công", {
          timeOut: 3000,
        });
  
        this.loadListOrder();
      } else {
        this.toastr.error(res.message, "Thất bại", {
          timeOut: 3000,
        });
      }
    }
    changeFilter(value: number) {
      this.selectedValueStatus = value;
      switch (this.selectedValueStatus) {
        case 1: {
          this.filter = true;
          this.status = true;
          this.headerTitle = "Đã được tiếp nhận";
          this.headerClass = "header-confirmed";
          break;
        }
        case 2: {
          this.filter = true;
          this.status = false;
          this.headerTitle = "Đang chờ được tiếp nhận";
          this.headerClass = "header-unconfirmed";  
          break;
        }
        default: {
          this.filter = false;
          this.status = false;
          this.headerTitle = "Quản lý đơn đặt hàng"; 
          this.headerClass = "header-default";
        }
      }
      this.loadListOrder();
    }
  
    loadListOrder(): void {
      this.orderService
        .list(this.store.id, this.pagi, this.filter, this.status)
        .subscribe({
          next: (res) => {
            if (res.isSuccess) {
              this.listOrder = res.data.list;
              this.pagi = res.data.pagination;
            } else {
              this.toastr.error(res.message, "Thất bại", {
                timeOut: 3000,
              });
            }
          },
          error: (err) => {
            this.toastr.error(err.error.message, "Thất bại", {
              timeOut: 3000,
            });
          },
        });
    }
  
    onChangePage(currentPage: any): void {
      this.pagi.currentPage = currentPage;
      this.loadListOrder();
    }
    openEditDialog(id: number): void {
      const dialogRef = this.dialog.open(FormEditComponent, {
        data: {
          idOrder: id,
          idStore: this.store.id,
        },
      });
      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          this.orderService.aceept(id).subscribe({
            next: (res) => {
              if (res.isSuccess) {
                this.toastr.success(res.message, "Thành công", {
                  timeOut: 3000,
                });
                this.loadListOrder();
              }
            },
          });
        }
      });
    }
  
}
