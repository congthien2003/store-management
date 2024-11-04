import { Injectable } from "@angular/core";
import * as signalR from "@microsoft/signalr";

@Injectable({
	providedIn: "root",
})
export class OrderHubService {
	private hubConnection: signalR.HubConnection;

	constructor() {
		this.hubConnection = new signalR.HubConnectionBuilder()
			.withUrl("https://localhost:7272/orderHub")
			.withAutomaticReconnect()
			.build();
		this.startConnection();

		this.hubConnection.onclose((error) => {
			console.error("SignalR connection closed", error);
			// Bạn có thể xử lý logic kết nối lại hoặc thông báo cho người dùng
		});
	}

	private startConnection() {
		this.hubConnection
			.start()
			.then(() => {
				console.log("Truy cập vào bàn");
			})
			.catch((err) =>
				console.log("Error while starting connection: " + err)
			);
	}

	public startConnectionStoreByTable(idTable: string) {
		this.hubConnection.invoke("JoinTableGroup", idTable);
	}

	ping(tableId: string) {
		return this.hubConnection.invoke("NotiTableGroup", tableId);
	}

	requestAccess(tableId: string, storeId: string) {
		return this.hubConnection.invoke("RequestAccess", tableId, storeId);
	}

	releaseAccess(tableId: string) {
		return this.hubConnection.invoke("ReleaseAccess", tableId);
	}

	// function ping hub when order a second

	onAccessGranted(callback: (tableId: string) => void) {
		this.hubConnection.on("AccessGranted", callback);
	}

	onAccessDenied(callback: (tableId: string) => void) {
		this.hubConnection.on("AccessDenied", callback);
	}

	onAccessReleased(callback: (tableId: string) => void) {
		this.hubConnection.on("AccessReleased", callback);
	}

	onReloadData(callback: (message: string) => void) {
		this.hubConnection.on("ReloadData", callback);
	}
}
