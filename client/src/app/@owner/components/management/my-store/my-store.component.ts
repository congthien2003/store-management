import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MatRadioModule } from "@angular/material/radio";
import { MatButtonModule } from "@angular/material/button";
import { MatTableModule } from "@angular/material/table";
import {
	MatDialog,
	MatDialogModule,
	MatDialogRef,
} from "@angular/material/dialog";
import { MatTooltipModule } from "@angular/material/tooltip";
import { NzButtonModule } from "ng-zorro-antd/button";
import { PaginationComponent } from "src/app/shared/components/pagination/pagination.component";
import { TablePagiComponent } from "src/app/shared/components/table-pagi/table-pagi.component";
import { RolePipe } from "src/app/core/utils/role.pipe";
import { SpinnerComponent } from "src/app/shared/components/spinner/spinner.component";
import {
	FormGroup,
	NonNullableFormBuilder,
	Validators,
	FormsModule,
} from "@angular/forms";
import { Pagination } from "src/app/core/models/common/Pagination";
import { ToastrService } from "ngx-toastr";
import { StoreService } from "src/app/core/services/store/store.service";
import { Store } from "src/app/core/models/interfaces/Store";
import { UserService } from "src/app/core/services/user/user.service";
import { ResolveStart } from "@angular/router";
import { FormEditComponent } from "./form-edit/form-edit.component";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";

const MatImport = [
	MatRadioModule,
	MatButtonModule,
	MatTableModule,
	MatDialogModule,
	MatTooltipModule,
	NzButtonModule,
];

@Component({
	selector: "app-my-store",
	standalone: true,
	templateUrl: "./my-store.component.html",
	styleUrls: ["./my-store.component.scss"],
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
				prop: "name",
				display: "Tên cửa hàng",
			},
			{
				prop: "address",
				display: "Địa chỉ",
			},
			{
				prop: "phone",
				display: "SDT",
			},
			{
				prop: "idUser",
				display: "Chủ sở hữu",
			},
		],
		hasAction: true,
	};
	isEditing: boolean = false;
	validateForm!: FormGroup;
	idStore!: number;
	nameUser!: string;
	myStore: {
		id?: number;
		name?: string;
		phone?: string;
		address?: string;
	} = {};

	idUser: number = 0;
	store!: Store;
	constructor(
		// public dialogRef: MatDialogRef<MyStoreComponent>,
		public dialog: MatDialog,
		private toastr: ToastrService,
		private storeService: StoreService,
		private fb: NonNullableFormBuilder,
		private authService: AuthenticationService
	) {}
	ngOnInit(): void {
		this.getMyStore();
	}
	getMyStore(): void {
		this.idUser = this.authService.getIdFromToken();
		this.store = JSON.parse(
			sessionStorage.getItem("storeInfo") ?? ""
		) as Store;
		this.storeService.getByIdUser(this.idUser).subscribe({
			next: (res) => {
				this.myStore = res.data;
				this.nameUser = res.data.userDTO.username;
			},
			error: (err) => {
				console.log(err);
			},
		});
	}
	toggleEdit() {
		if (this.isEditing) {
			this.onSubmit();
		}
		this.isEditing = !this.isEditing;
	}
	onSubmit(): void {
		const idUser = this.authService.getIdFromToken();
		const id = JSON.parse(localStorage.getItem("idStore") ?? "");
		const dataToSave = {
			id: id,
			name: this.myStore.name,
			address: this.myStore.address,
			phone: this.myStore.phone,
			idUser: idUser,
		};
		this.storeService.update(dataToSave, id).subscribe({
			next: (res) => {
				if (res.isSuccess) {
					this.toastr.success(res.message, "Thành công", {
						timeOut: 3000,
					});
				} else {
					this.toastr.error(res.message, "Thất bại", {
						timeOut: 3000,
					});
				}
			},
			error: (err) => {
				this.toastr.error(err.message || "Có lỗi xảy ra", "Thất bại", {
					timeOut: 3000,
				});
			},
		});
	}
}
