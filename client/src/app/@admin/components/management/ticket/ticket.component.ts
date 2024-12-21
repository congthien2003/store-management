import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Pagination } from 'src/app/core/models/interfaces/Common/Pagination';

import { PaginationComponent } from '../../../../shared/components/pagination/pagination.component';
import { ModalDeleteComponent } from 'src/app/shared/components/modal-delete/modal-delete.component';
import { FormEditComponent } from './form-edit/form-edit.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { TicketService } from 'src/app/core/services/ticket.service';
import { UserService } from 'src/app/core/services/user/user.service';
import { MatMenuModule } from '@angular/material/menu';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { debounceTime, distinctUntilChanged, Subject, timeout } from 'rxjs';
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
  selector: 'app-ticket',
  standalone: true,
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.scss'],
  imports: [CommonModule, PaginationComponent, MatImport],
})
export class TicketComponent implements OnInit {
  config = {
    displayedColumns: [
      {
        prop: 'id',
        display: 'STT',
      },
      {
        prop: 'username',
        display: 'Tên người dùng',
      },
      {
        prop: 'title',
        display: 'Tiêu đề',
      },
      {
        prop: 'CreatedAt',
        display: 'Thời gian',
      },
      {
        prop: 'Status',
        display: 'Trạng thái',
      },
    ],
    hasAction: true,
  };
  pagi: Pagination = {
    totalPage: 0,
    totalRecords: 0,
    currentPage: 1,
    pageSize: 5,
    hasNextPage: false,
    hasPrevPage: false,
  };
  listTicket!: any[];
  private searchSubject = new Subject<string>();
  searchTerm: string = '';
  status: number = 0;
  nameUserCache = new Map<number, string>();
  constructor(
    public dialog: MatDialog,
    private toastr: ToastrService,
    private ticketService: TicketService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.loadListTicket(this.status);
  }
  loadListTicket(status: number): void {
    this.ticketService.getAll(status, this.pagi).subscribe({
      next: (res) => {
        this.listTicket = res.data.list;
        console.log(this.listTicket);

        this.pagi = res.data.pagination;
        if (this.pagi.totalPage === 0) {
          console.warn('No pages available');
          return;
        }
        if (this.pagi.currentPage > this.pagi.totalPage) {
          this.pagi.currentPage = 1;
          this.loadListTicket(status);
        }
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
  openEditDialog(id: number): void {
    const dialogRef = this.dialog.open(FormEditComponent, {
      data: { id: id },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadListTicket(this.status);
      }
    });
  }
  openDeleteDialog(id: number): void {
    const dialogRef = this.dialog.open(ModalDeleteComponent, {
      data: { id: id },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result === true) {
        this.handleDelete(id);
      }
    });
  }
  handleDelete(id: number): void {
    this.ticketService.delete(id).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.toastr.success(res.message, 'Xóa thành công', {
            timeOut: 3000,
          });
          this.loadListTicket(this.status);
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
  onChangePage(currentPage: any): void {
    console.log(currentPage);
    this.pagi.currentPage = currentPage;
    this.loadListTicket(this.status);
  }
  onSearchTerm(): void {
    this.searchSubject.next(this.searchTerm);
  }

  search(searchTerm: string): void {
    this.loadListTicket(this.status);
    console.log(searchTerm);
  }
  getNameUser(id: number): string {
    if (this.nameUserCache.has(id)) {
      return this.nameUserCache.get(id) || 'Unknown User';
    }

    this.userService.getById(id).subscribe({
      next: (res) => {
        this.nameUserCache.set(id, res.data.username);
      },
      error: (err) => {
        console.log(err);
        this.nameUserCache.set(id, 'Unknown User');
      },
    });
    return 'Loading...';
  }
}
