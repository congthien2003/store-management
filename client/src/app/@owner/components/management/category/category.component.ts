import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { Pagination } from 'src/app/core/models/common/Pagination';
import { ModalDeleteComponent } from 'src/app/shared/components/modal-delete/modal-delete.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from 'src/app/core/services/category.service';
import { debounce, debounceTime, distinctUntilChanged, Subject } from 'rxjs';
import { FormAddComponent } from './form-add/form-add.component';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { TablePagiComponent } from 'src/app/shared/components/table-pagi/table-pagi.component';
import { FormEditComponent } from './form-edit/form-edit.component';
import { SpinnerComponent } from 'src/app/shared/components/spinner/spinner.component';
import { RolePipe } from 'src/app/core/utils/role.pipe';
import { FormsModule } from '@angular/forms';
import { Category } from 'src/app/core/models/interfaces/Category';

const MatImport = [
  MatRadioModule,
  MatButtonModule,
  MatTableModule,
  MatDialogModule,
  MatTooltipModule,
  NzButtonModule,
];

@Component({
  selector: 'app-category',
  standalone: true,
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss'],
  imports: [
    CommonModule,
    MatImport,
    PaginationComponent,
    TablePagiComponent,
    FormEditComponent,
    FormAddComponent,
    SpinnerComponent,
    RolePipe,
    FormsModule,
  ],
})
export class CategoryComponent implements OnInit {
  config = {
    displayedColumns: [
      {
        prop: 'id',
        display: 'STT',
      },
      {
        prop: 'name',
        display: 'Tên thể loại',
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
  searchTerm: string = '';
  listCategory!: Category[];
  idStore!: number;

  private searchSubject = new Subject<string>();
  constructor(
    public dialog: MatDialog,
    private toastr: ToastrService,
    private categoryService: CategoryService
  ) {
    this.searchSubject
      .pipe(debounceTime(1500), distinctUntilChanged())
      .subscribe((searchTerm) => {
        this.search(searchTerm);
      });
  }
  ngOnInit(): void {
    this.loadListCategory();
  }

  loadListCategory(): void {
    this.idStore = JSON.parse(localStorage.getItem('idStore') ?? '');
    this.categoryService
      .list(this.idStore, this.pagi, this.searchTerm)
      .subscribe({
        next: (res) => {
          this.listCategory = res.data.list;
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
    this.loadListCategory();
  }
  openAddDialog(): void {
    const dialogRef = this.dialog.open(FormAddComponent, {
      data: { idStore: this.idStore },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadListCategory();
      }
    });
  }
  openEditDialog(id: number): void {
    const dialogRef = this.dialog.open(FormEditComponent, {
      data: { id: id },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadListCategory();
      }
    });
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
    this.categoryService.delete(id).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.toastr.success(res.message, 'Xóa thành công', {
            timeOut: 3000,
          });
          this.loadListCategory();
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
  onSearchTerm(): void {
    this.searchSubject.next(this.searchTerm);
  }

  search(searchTerm: string): void {
    this.loadListCategory();
    console.log(searchTerm);
  }
}
