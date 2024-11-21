import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { StoreService } from "src/app/core/services/store/store.service";
import { Store } from "src/app/core/models/interfaces/Store";
import { FormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { BankInfo } from "src/app/core/models/interfaces/BankInfo";
import { BankInfoService } from "src/app/core/services/store/bank-info.service";
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { ViewQrComponent } from "src/app/shared/components/view-qr/view-qr.component";

@Component({
	selector: "app-my-store",
	standalone: true,
	imports: [CommonModule, FormsModule, MatButtonModule, MatDialogModule],
	templateUrl: "./my-store.component.html",
	styleUrls: ["./my-store.component.scss"],
})
export class MyStoreComponent implements OnInit {
	idUser: number = 0;
	idStore: number = 0;
	store!: Store;
	listBankInfo: BankInfo[] = [
		{
			bankId: "MB",
			accountName: "BUI CONG THIEN",
			accountId: "3000017112003",
			id: 1,
			storeId: 1,
			bankName: "MBank",
		},
	];
	isEditing = false; // Biến để điều khiển chế độ chỉnh sửa

	constructor(
		private authService: AuthenticationService,
		private storeService: StoreService,
		private bankInfoService: BankInfoService,
		public dialog: MatDialog
	) {}

	ngOnInit(): void {
		this.idUser = this.authService.getIdFromToken();
		this.store = JSON.parse(
			sessionStorage.getItem("storeInfo") ?? ""
		) as Store;
		this.getListBankInfo();
	}

	// Hàm để bật/tắt chế độ chỉnh sửa
	toggleEdit() {
		this.isEditing = !this.isEditing;
	}

	getListBankInfo() {
		this.bankInfoService.getAllByIdStore(this.store.id).subscribe((res) => {
			if (res.isSuccess) {
				console.log(res.data);

				this.listBankInfo = res.data;
			}
		});
	}

	editBankInfo(item: BankInfo) {}

	reviewQR(item: BankInfo) {
		const url = this.bankInfoService.generateQR(item, 500000);
		console.log(url);
		const dialogRef = this.dialog.open(ViewQrComponent, {
			data: {
				url: url,
			},
		});
		dialogRef.afterClosed().subscribe((result) => {});
	}
}
