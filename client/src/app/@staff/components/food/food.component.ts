import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { PricePipe } from 'src/app/core/utils/price.pipe';
import { CategoryPipe } from 'src/app/core/utils/category.pipe';
import { FormsModule } from '@angular/forms';
import { SpinnerComponent } from 'src/app/shared/components/spinner/spinner.component';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { Pagination } from 'src/app/core/models/interfaces/Common/Pagination';
import { Category } from 'src/app/core/models/interfaces/Category';
import { Food } from 'src/app/core/models/interfaces/Food';
import { Store } from 'src/app/core/models/interfaces/Store';
import { ToastrService } from 'ngx-toastr';
import { FoodService } from 'src/app/core/services/store/food.service';
import { CategoryService } from 'src/app/core/services/store/category.service';
import { FirebaseService } from 'src/app/core/services/firebase.service';
import { LoaderService } from 'src/app/core/services/loader.service';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';
import { MatMenuModule } from '@angular/material/menu';


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
  selector: 'app-food',
  standalone: true,
  imports: [CommonModule,
		MatImport,
		PaginationComponent,
		SpinnerComponent,
		FormsModule,
		CategoryPipe,
		PricePipe,
  ],
  templateUrl: './food.component.html',
  styleUrls: ['./food.component.scss']
})
export class FoodComponent {
    config = {
      displayedColumns: [
        {
          prop: "id",
          display: "STT",
        },
        {
          prop: "images",
          display: "Hình ảnh",
        },
        {
          prop: "name",
          display: "Tên",
        },
        {
          prop: "price",
          display: "Giá",
        },
        // {
        // 	prop: "idCateogry",
        // 	display: "Loại món ăn",
        // },
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
    selectedValueStatus: number = 0;
    searchTerm: string = "";
	  listFood!: Food[];
	  listCategory!: Category[];
	  store!: Store;
    filter: boolean = false;
    categoryId: number | null = null;
    private searchSubject = new Subject<string>();
	constructor(
		public dialog: MatDialog,
		private toastr: ToastrService,
		private foodService: FoodService,
		private categoryService: CategoryService,
		private firebaseSerivce: FirebaseService,
		private loader: LoaderService
	) {
		this.searchSubject
			.pipe(debounceTime(1500), distinctUntilChanged())
			.subscribe((searchTerm) => {
				this.search(searchTerm);
			});
	}
	ngOnInit(): void {
		this.store = JSON.parse(
			sessionStorage.getItem("storeInfo") ?? ""
		) as Store;

		this.loadListCategory();
		this.loadListFood();
	}

	loadListFood(): void {
		console.log("Load list food");

		this.foodService
			.list(this.store.id, this.pagi, this.searchTerm,"", true ,this.filter, this.categoryId ?? undefined)
			.subscribe({
				next: (res) => {
					console.log(res);
					this.listFood = res.data.list;
					this.pagi = res.data.pagination;
					if (this.pagi.currentPage > this.pagi.totalPage) {
						this.pagi.currentPage = 1;
						this.loadListFood();
					}
				},
				error: (err) => {
					console.log(err);
				},
			});
	}
  changeFilter(value: number){
		this.selectedValueStatus = value;
		switch (this.selectedValueStatus) {
			case 1:
				this.categoryId = 1; 
				this.filter = false;
				break;
			case 2:
				this.categoryId = 2; 
				this.filter = false;
				break;
      		case 3:
				this.categoryId = 3; 
				this.filter = false;
				break;
      		case 4:
        		this.categoryId = 4; 
        		this.filter = false;
        	break;
     		 case 5:
       			this.categoryId = 5; 
        		this.filter = false;
       			break;
			default:
				this.categoryId = null; 
				this.filter = true;
				break;
		}
		this.loadListFood(); 
	}

	getCategoryName(id: number): string {
		return this.listCategory.find((e) => e.id === id)?.name ?? "Unknown";
	}

	loadListCategory(): void {
		this.categoryService.getAllByIdStore(this.store.id).subscribe({
			next: (res) => {
				this.listCategory = res.data;
			},
			error: (err) => {
				console.log(err);
			},
		});
	}

	onChangePage(currentPage: any): void {
		this.pagi.currentPage = currentPage;
		this.loadListFood();
	}

	onSearchTerm(): void {
		this.searchSubject.next(this.searchTerm);
	}

	search(searchTerm: string): void {
		this.loadListFood();
		console.log(searchTerm);
	}
}
