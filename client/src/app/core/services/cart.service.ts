import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
	providedIn: "root",
})
export class CartService {
	private cartItems = new BehaviorSubject<number>(0);
	cartItems$ = this.cartItems.asObservable();
	constructor() {
		const items = this.getItemsFromSession();
		this.cartItems.next(items);
	}

	getItemsFromSession() {
		const sessionData = sessionStorage.getItem("ItemID")?.split(",").length;
		return sessionData ?? 0;
	}

	updateCart() {
		const sessionData = sessionStorage.getItem("ItemID")?.split(",").length;
		this.cartItems.next(sessionData ?? 0);
	}
}
