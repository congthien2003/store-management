import { Injectable, signal } from "@angular/core";

@Injectable({
	providedIn: "root",
})
export class NotificationClientService {
	private storageKey = "notifications";
	private expirationTime = 15 * 60 * 1000; // 15 phút (tính bằng milliseconds)
	notificationCount = signal<number>(0);
	constructor() {
		// Xóa các thông báo cũ khi service khởi tạo
		this.clearExpiredNotifications();
		this.updateNotificationCount();
	}

	/**
	 * Load danh sách thông báo từ localStorage
	 */
	loadList(): any[] {
		const notifications = JSON.parse(
			localStorage.getItem(this.storageKey) || "[]"
		);
		return notifications;
	}

	/**
	 * Thêm một thông báo mới vào localStorage
	 * @param notification Thông báo cần thêm
	 */
	addNewNoti(notification: { message: string; [key: string]: any }) {
		const notifications = this.loadList();

		// Thêm thông báo mới kèm timestamp
		notifications.push({
			...notification,
			timestamp: new Date().getTime(),
		});

		// Lưu lại vào localStorage
		localStorage.setItem(this.storageKey, JSON.stringify(notifications));

		// Dọn dẹp thông báo cũ
		this.clearExpiredNotifications();
		this.updateNotificationCount();
	}

	/**
	 * Xóa các thông báo đã hết hạn (quá 15 phút)
	 */
	clearExpiredNotifications() {
		const now = new Date().getTime();
		const notifications = this.loadList();

		// Lọc các thông báo còn trong thời hạn
		const validNotifications = notifications.filter(
			(notification: any) =>
				now - notification.timestamp <= this.expirationTime
		);

		// Lưu lại danh sách đã lọc vào localStorage
		localStorage.setItem(
			this.storageKey,
			JSON.stringify(validNotifications)
		);
	}

	/**
	 * Xóa tất cả thông báo khỏi localStorage
	 */
	clearAll() {
		localStorage.removeItem(this.storageKey);
	}

	private updateNotificationCount() {
		const notifications = this.loadList();
		this.notificationCount.set(notifications.length);
	}
}
