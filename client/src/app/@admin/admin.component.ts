import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ActivatedRoute, Router, RouterOutlet } from "@angular/router";
import { AuthenticationService } from "../core/services/auth/authentication.service";

@Component({
	selector: "app-admin",
	standalone: true,
	imports: [CommonModule, RouterOutlet],
	templateUrl: "./admin.component.html",
	styleUrls: ["./admin.component.scss"],
})
export class AdminComponent {
	currentURL: string = "dashboard";
	activeIndex: number = 0;

	constructor(
		private router: Router,
		private authService: AuthenticationService
	) {
		const savedIndex = sessionStorage.getItem("activeIndex");
		if (savedIndex) {
			this.activeIndex = parseInt(savedIndex, 10);
			// Điều hướng đến route tương ứng nếu cần thiết
			this.swithRoute(this.activeIndex);
		}
	}

	swithRoute(index: number) {
		this.activeIndex = index;
		sessionStorage.setItem("activeIndex", index.toString());
		switch (index) {
			case 0: {
				this.router.navigate(["/admin/dashboard"]);
				break;
			}
			case 1: {
				this.router.navigate(["/admin/user"]);
				break;
			}
			case 2: {
				this.router.navigate(["/admin/store"]);
				break;
			}
			case 3: {
				this.authService.logout();
				this.router.navigate(["/auth/login"]);
				break;
			}
		}
	}

	bindActiveMenu(url: string) {
		switch (url) {
			case "/pages": {
				this.activeIndex = 0;
				break;
			}
			case "/pages/create": {
				this.activeIndex = 1;
				break;
			}
			case "/pages/find": {
				this.activeIndex = 2;
				break;
			}
			default: {
				this.activeIndex = -1;
				break;
			}
		}
	}

	logOut() {
		localStorage.removeItem("token");
		this.router.navigate(["/auth/login"]);
	}
}
