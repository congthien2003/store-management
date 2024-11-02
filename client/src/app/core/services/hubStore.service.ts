import { Injectable } from "@angular/core";
import * as signalR from "@microsoft/signalr";
import { Store } from "../models/interfaces/Store";
import { ToastrService } from "ngx-toastr";
import { AuthenticationService } from "./auth/authentication.service";
import { StoreService } from "./store/store.service";
@Injectable({
	providedIn: "root",
})
export class HubService {
	private hubConnection: signalR.HubConnection;

	store!: Store;

	constructor(
		private toastr: ToastrService,
		private authService: AuthenticationService,
		private storeService: StoreService
	) {
		const storeValue = sessionStorage.getItem("storeInfo") ?? null;

		if (storeValue) {
			this.store = JSON.parse(storeValue);
		} else {
			this.storeService
				.getByIdUser(this.authService.getIdFromToken())
				.subscribe({
					next: (res) => {
						if (res.isSuccess) {
							this.store = res.data;
							sessionStorage.setItem(
								"storeInfo",
								JSON.stringify(res.data)
							);
						} else {
							this.toastr.warning(res.message, "Thông báo", {
								timeOut: 3000,
							});
						}
					},
				});
		}

		this.hubConnection = new signalR.HubConnectionBuilder()
			.withUrl("https://localhost:7272/orderHub")
			.build();

		this.startConnectionStore();
		this.addNotificationListener();
	}

	public startConnectionStore() {
		this.hubConnection
			.start()
			.then(() => {
				// Join the store group to receive notifications
				this.hubConnection.invoke(
					"JoinStoreGroup",
					this.store.id.toString()
				);
			})
			.catch((err) =>
				console.log("Error while starting SignalR connection: " + err)
			);
	}

	public startConnectionStoreByTable(idTable: string) {
		this.hubConnection.invoke("JoinTableGroup", idTable);
	}

	private addNotificationListener() {
		this.hubConnection.on("ReceiveNotification", (message: string) => {
			console.log("Receive Noti");

			this.toastr.info(message, "Thông báo", {
				timeOut: 3000,
			});
		});
		this.hubConnection.on(
			"ReceiveNotificationAccessTable",
			(url: string) => {
				console.log("Receive Noti Access Table");
				this.startConnectionStoreByTable(url);
				this.toastr.info("Có bàn truy cập !", "Thông báo", {
					timeOut: 3000,
				});
			}
		);
		this.hubConnection.on("ReceiveDisconectAccess", (url: string) => {
			console.log("Receive Noti Disconect Access Table");
			console.log(url);
			this.toastr.info("Có bàn đóng truy cập !", "Thông báo", {
				timeOut: 3000,
			});
		});
		this.hubConnection.on("RequestAccess", (url: string) => {
			console.log("Có bàn đang muốn truy cập");
			console.log(url);
			localStorage.setItem("AcessToken", url);
			this.toastr.info("Có bàn truy cập !", "Thông báo", {
				timeOut: 3000,
			});
		});
	}

	onReloadData(callback: (message: string) => void) {
		this.hubConnection.on("ReloadData", callback);
	}
}
