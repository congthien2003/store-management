import {
	AfterContentInit,
	AfterViewInit,
	Component,
	HostListener,
	OnInit,
} from "@angular/core";
import { CommonModule } from "@angular/common";
import {
	ActivatedRoute,
	ActivatedRouteSnapshot,
	Resolve,
	Router,
} from "@angular/router";
import { StoreService } from "src/app/core/services/store/store.service";
import { OrderService } from "src/app/core/services/store/order.service";
import { FoodService } from "src/app/core/services/store/food.service";
import { CategoryService } from "src/app/core/services/store/category.service";
import { OrderDetailService } from "src/app/core/services/store/order-detail.service";
import { Store } from "src/app/core/models/interfaces/Store";
import { Category } from "src/app/core/models/interfaces/Category";
import { Food } from "src/app/core/models/interfaces/Food";
import { FormsModule } from "@angular/forms";
import { Pagination } from "src/app/core/models/interfaces/Common/Pagination";
import { CategoryPipe } from "src/app/core/utils/category.pipe";
import { PricePipe } from "src/app/core/utils/price.pipe";
// Import Mat
import { MatChipsModule } from "@angular/material/chips";
import { MatButtonModule } from "@angular/material/button";
import { MatBadgeModule } from "@angular/material/badge";
import { ToastrService } from "ngx-toastr";
import { OrderDetail } from "src/app/core/models/interfaces/OrderDetail";
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { CartComponent } from "../cart/cart.component";
import { Order } from "src/app/core/models/interfaces/Order";
import { PaginationComponent } from "src/app/shared/components/pagination/pagination.component";
import { HubService } from "src/app/core/services/hubStore.service";
import { TableService } from "src/app/core/services/store/table.service";
import { SpinnerComponent } from "src/app/shared/components/spinner/spinner.component";
import { LoaderService } from "src/app/core/services/loader.service";
import { Observable, catchError, forkJoin, of } from "rxjs";
import { OrderHubService } from "src/app/core/services/order-hub.service";
import { OrderAcessService } from "src/app/core/services/order-acess.service";
import { OrderAccessToken } from "src/app/core/models/interfaces/OrderAccessToken";
import { OrderDetailResponse } from "src/app/core/models/interfaces/Response/OrderDetailResponse";
import { BankInfo } from "src/app/core/models/interfaces/BankInfo";
import { ViewQrComponent } from "src/app/shared/components/view-qr/view-qr.component";
import { BankInfoService } from "src/app/core/services/store/bank-info.service";
import { PopupSubmitOrderComponent } from "src/app/shared/components/popup-submit-order/popup-submit-order.component";
import { MatMenuModule } from "@angular/material/menu";

const MatImport = [
	MatButtonModule,
	MatChipsModule,
	MatBadgeModule,
	MatDialogModule,
	MatMenuModule,
];

@Component({
	selector: "app-order",
	standalone: true,
	imports: [
		CommonModule,
		MatImport,
		FormsModule,
		CategoryPipe,
		PricePipe,
		PaginationComponent,
		SpinnerComponent,
	],
	templateUrl: "./order.component.html",
	styleUrls: ["./order.component.scss"],
})
export class OrderComponent implements OnInit {
	idStore: string = "";
	idTable: string = "";

	store!: Store;
	listCategory: Category[] = [];
	listFood: Food[] = [];
	listCombo: any[] = [];
	listBestSeller: Food[] = [
		{
			id: 1,
			name: "Cà phê sữa đá abcas",
			status: false,
			price: 45000,
			imageUrl:
				"https://firebasestorage.googleapis.com/v0/b/storemanagement-5e1b4.appspot.com/o/imagesClient%2Fstore_1%2Ffoods%2F1730692999491_cfsua.jpg?alt=media&token=6810e95e-6561-48ff-adc6-9dfe32fc1445",
			idCategory: 2,
		},
		{
			id: 2,
			name: "Cà phê đen",
			status: false,
			price: 45000,
			imageUrl:
				"https://firebasestorage.googleapis.com/v0/b/storemanagement-5e1b4.appspot.com/o/imagesClient%2Fstore_1%2Ffoods%2F1730693016115_cfden.jpg?alt=media&token=1a518d5b-a533-40a7-9023-cd6f77eabe4d",
			idCategory: 2,
		},
		{
			id: 3,
			name: "Cà phê muối",
			status: false,
			price: 50000,
			imageUrl:
				"https://firebasestorage.googleapis.com/v0/b/storemanagement-5e1b4.appspot.com/o/imagesClient%2Fstore_1%2Ffoods%2F1730693029979_cfmuoi.png?alt=media&token=9114ee90-a973-4516-a3ab-6d0aa9e1cbaa",
			idCategory: 2,
		},
		{
			id: 3,
			name: "Cà phê muối",
			status: false,
			price: 50000,
			imageUrl:
				"https://firebasestorage.googleapis.com/v0/b/storemanagement-5e1b4.appspot.com/o/imagesClient%2Fstore_1%2Ffoods%2F1730693029979_cfmuoi.png?alt=media&token=9114ee90-a973-4516-a3ab-6d0aa9e1cbaa",
			idCategory: 2,
		},
		{
			id: 3,
			name: "Cà phê muối",
			status: false,
			price: 50000,
			imageUrl:
				"https://firebasestorage.googleapis.com/v0/b/storemanagement-5e1b4.appspot.com/o/imagesClient%2Fstore_1%2Ffoods%2F1730693029979_cfmuoi.png?alt=media&token=9114ee90-a973-4516-a3ab-6d0aa9e1cbaa",
			idCategory: 2,
		},
		{
			id: 3,
			name: "Cà phê muối",
			status: false,
			price: 50000,
			imageUrl:
				"https://firebasestorage.googleapis.com/v0/b/storemanagement-5e1b4.appspot.com/o/imagesClient%2Fstore_1%2Ffoods%2F1730693029979_cfmuoi.png?alt=media&token=9114ee90-a973-4516-a3ab-6d0aa9e1cbaa",
			idCategory: 2,
		},
	];
	pagiFood: Pagination = {
		totalPage: 0,
		totalRecords: 0,
		currentPage: 1,
		pageSize: 5,
		hasNextPage: false,
		hasPrevPage: false,
	};

	search: string = "";
	selectedCategory: number = 0;
	listOrderDetail: OrderDetail[] = [];
	listOrdered: OrderDetailResponse[] = [];

	order: Order = {
		id: 0,
		total: 0,
		status: false,
		hasInvoice: false,
		createdAt: new Date(),
		idTable: 0,
		idStore: 0,
	};

	deninedOrder: boolean = false;
	submitOrderStatus: boolean = false;

	constructor(
		private activeRoute: ActivatedRoute,
		private storeService: StoreService,
		private foodService: FoodService,
		private categoryService: CategoryService,
		private tableService: TableService,
		private orderService: OrderService,
		private orderDetailService: OrderDetailService,
		private orderAccessService: OrderAcessService,
		private toastr: ToastrService,
		private dialog: MatDialog,
		private router: Router,
		private orderHub: OrderHubService,
		private bankInfoService: BankInfoService
	) {}

	ngOnInit(): void {
		this.idStore = this.activeRoute.snapshot.params["idStore"];
		this.idTable = this.activeRoute.snapshot.params["idTable"];
		forkJoin({
			store: this.storeService.getByGuId(this.idStore),
			table: this.tableService.getByGuId(this.idTable),
		}).subscribe({
			next: (res) => {
				if (res.store.isSuccess && res.table.isSuccess) {
					this.store = res.store.data;
					this.order.idStore = this.store.id;
					this.order.idTable = res.table.data.id;

					setTimeout(
						() => this.requestAccess(this.idTable, this.idStore),
						2000
					);

					this.orderHub.onAccessGranted((tableId) => {
						if (this.idTable === tableId) {
							this.deninedOrder = false;
							console.log("Cho phép truy cập vào bàn", tableId);
							setTimeout(() => {
								this.orderHub.startConnectionStoreByTable(
									this.idTable
								);
							}, 1000);

							this.loadCategory();
							this.loadListFood();
							this.loadListCombo();
							this.loadToken(this.idTable, this.idStore);
						}
					});

					this.orderHub.onAccessDenied((tableId) => {
						if (this.idTable === tableId) {
							this.deninedOrder = true;
							console.log("Không cho truy cập vào bàn", tableId);
							this.router.navigateByUrl("/order/denied");
						}
					});

					this.orderHub.onReloadData((message) => {
						this.toastr.info(message);
						this.loadListOrdered();
					});

					this.orderHub.onReceiveUpdateStatusOrder((message) => {
						this.loadListOrdered();
					});

					this.orderHub.ping(this.idTable);
				} else {
					this.router.navigateByUrl("/order/denied");
				}
			},
			error: (err) => {
				console.log(err.error.message);
				this.toastr.error(err.error.message, "Thông báo !", {
					timeOut: 3000,
					positionClass: "toast-bottom-center",
				});
				setTimeout(() => {
					this.router.navigateByUrl("/order/denied");
				}, 1000);
			},
		});
	}

	loadListFood(): void {
		this.foodService.list(this.store.id, this.pagiFood).subscribe({
			next: (res) => {
				if (res.isSuccess) {
					this.listFood = res.data.list;
					this.pagiFood = res.data.pagination;
				}
			},
		});
	}

	loadListCombo(): void {
		this.foodService.listCombo(this.store.id).subscribe({
			next: (res) => {
				this.listCombo = res;
				console.log(this.listCombo);
			},
		});
	}

	loadCategory(): void {
		this.pagiFood.currentPage = 1;
		this.categoryService.getAllByIdStore(this.store.id).subscribe({
			next: (res) => {
				if (res.isSuccess) {
					this.listCategory = res.data;
				}
			},
		});
	}

	onChangePage(currentPage: any): void {
		this.pagiFood.currentPage = currentPage;
		this.loadListFood();
	}

	onChangeCategory(id: number): void {
		this.selectedCategory = id;
		if (this.selectedCategory === 0) {
			this.loadListFood();
		} else {
			this.foodService
				.getByCategory(this.selectedCategory, this.pagiFood)
				.subscribe({
					next: (res) => {
						if (res.isSuccess) {
							this.listFood = res.data.list;
							this.pagiFood = res.data.pagination;
						} else {
							console.log(res.message);
						}
					},
				});
		}
	}

	addToCart(id: number): void {
		let hasInList = false;
		for (let i = 0; i < this.listOrderDetail.length; i++) {
			if (this.listOrderDetail[i].idFood === id) {
				this.listOrderDetail[i].quantity++;
				hasInList = true;
				break;
			}
		}
		if (hasInList === false) {
			this.listOrderDetail.push({
				idFood: id,
				quantity: 1,
				idOrder: 0,
				statusProcess: 1,
			});
		}

		this.toastr.success("Thêm thành công", "Thành công", {
			timeOut: 2000,
			positionClass: "toast-bottom-center",
		});
	}

	openCart(): void {
		const dialogRef = this.dialog.open(CartComponent, {
			data: {
				listOrderDetail: this.listOrderDetail,
				listFood: this.listFood,
				listCategory: this.listCategory,
				submitOrderStatus: this.submitOrderStatus,
				listOrdered: this.listOrdered,
			},
		});

		dialogRef.afterClosed().subscribe((result) => {
			this.listOrderDetail = result.listOrderDetail;
			if (result.submitOrderStatus) {
				this.submitOrderStatus = result.submitOrderStatus;
				this.order.total = result.totalOrder;
				// Nếu trước đó không order
				if (!result.updateOrder) {
					this.submitOrderStatus = result.submitOrderStatus;
					this.order.total = result.totalOrder;
					this.orderService.create(this.order).subscribe({
						next: (res) => {
							if (res.isSuccess) {
								this.order = res.data;

								this.listOrderDetail.forEach((e) => {
									e.idOrder = this.order.id;
								});

								const dialogSuccessRef = this.dialog.open(
									PopupSubmitOrderComponent,
									{
										data: {},
									}
								);

								dialogSuccessRef
									.afterClosed()
									.subscribe((result) => {
										if (result == "history") {
										}
									});

								// Khi tạo order thành công -> Tạo token
								this.createOrderAccessToken(this.idTable);

								this.orderDetailService
									.create(this.listOrderDetail)
									.subscribe({
										next: (res) => {
											if (res.isSuccess) {
												console.log(res);
												this.loadListOrdered();
											}
										},
									});
							}
						},
					});
				} else {
					console.log(this.order);

					this.orderService
						.update(this.order.id, this.order)
						.subscribe({
							next: (res) => {
								if (res.isSuccess) {
									this.order = res.data;

									this.listOrderDetail.forEach((e) => {
										e.idOrder = this.order.id;
									});
									this.toastr.success(
										res.message,
										"Thông báo",
										{
											timeOut: 2500,
										}
									);

									this.orderDetailService
										.create(this.listOrderDetail)
										.subscribe({
											next: (res) => {
												if (res.isSuccess) {
													console.log(res.data);

													this.loadListOrdered();
												}
											},
										});
								}
							},
						});
				}
			}
		});
	}

	loadListOrdered(): void {
		this.orderDetailService.listNoPagi(this.order.id).subscribe({
			next: (res) => {
				if (res.isSuccess) {
					// Sau khi order -> Load lại mảng rỗng
					this.listOrderDetail = [];
					this.listOrdered = res.data;
				}
			},
		});
	}

	onNoClick(): void {}

	requestAccess(idTable: string, idStore: string) {
		this.orderHub.requestAccess(idTable, idStore);
	}

	releaseAccess(idTable: string) {
		this.orderHub.releaseAccess(idTable);
	}

	// Load token from BE
	loadToken(URL: string, idStore: string): void {
		this.orderAccessService.getByURL(URL, idStore).subscribe({
			next: (res) => {
				if (res.isSuccess) {
					console.log("Có tồn tại token");
					this.order.id = res.data.idOrder;
					this.submitOrderStatus = true;
					this.orderService.getById(this.order.id).subscribe({
						next: (res) => {
							if (res.isSuccess) {
								console.log(res.data);
								this.order = res.data;

								this.orderDetailService
									.list(this.order.id, this.pagiFood)
									.subscribe({
										next: (res) => {
											if (res.isSuccess) {
												this.listOrdered =
													res.data.list;
												this.pagiFood =
													res.data.pagination;
											}
										},
									});
							}
						},
					});
				}
			},
			error: (error) => {
				console.log("Chưa có token");
				console.log(error.error);
			},
		});
	}

	// Create Token
	createOrderAccessToken(URL: string): void {
		const data: any = {
			qrURL: URL,
			idOrder: this.order.id,
			isActive: true,
		};
		this.orderAccessService.create(data).subscribe({
			next: (res) => {
				if (res.isSuccess) {
					console.log("Tạo thành công !");
				}
			},
		});
	}

	bankInfos: BankInfo[] = [];
	reviewQR() {
		this.bankInfoService.getAllByIdStore(this.store.id).subscribe({
			next: (res) => {
				if (res.isSuccess) {
					console.log(res.data);
					this.bankInfos = res.data;
					const url = this.bankInfoService.generateQR(
						this.bankInfos[0],
						500000
					);
					console.log(url);
					const dialogQRRef = this.dialog.open(ViewQrComponent, {
						data: {
							url: url,
						},
					});
					dialogQRRef.afterClosed().subscribe((result) => {});
				} else {
					console.log("Error");
				}
			},
		});
	}

	// Request Call Staff
	requestCallStaff() {
		console.log("Request Call Staff", this.idTable, this.idStore);
		this.orderHub.requestCallStaff(this.idTable, this.idStore);
		this.toastr.info("Vui lòng chờ trong giây lát!", "Thông báo", {
			timeOut: 3000,
			positionClass: "toast-bottom-center",
		});
	}
}
