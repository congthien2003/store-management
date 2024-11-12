import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MatButtonModule } from "@angular/material/button";
import { MatCommonModule } from "@angular/material/core";
import { MatMenuModule } from "@angular/material/menu";
import { RouterOutlet, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Subscription } from "rxjs";
import { NotfoundStoreComponent } from "../@owner/components/notfound-store/notfound-store.component";
import { Store } from "../core/models/interfaces/Store";
import { AuthenticationService } from "../core/services/auth/authentication.service";
import { HubService } from "../core/services/hubStore.service";
import { StoreService } from "../core/services/store/store.service";
import { UserService } from "../core/services/user/user.service";
import { SpinnerComponent } from "../shared/components/spinner/spinner.component";
const MatModuleImport = [MatButtonModule, MatCommonModule, MatMenuModule];
@Component({
	selector: "app-staff",
	standalone: true,
	imports: [
		CommonModule,
		MatModuleImport,
		RouterOutlet,
		NotfoundStoreComponent,
		SpinnerComponent,
	],
	templateUrl: "./staff.component.html",
	styleUrls: ["./staff.component.scss"],
})
export class StaffComponent {
	currentURL: string = "dashboard";
	activeIndex: number = 0;
	idUser: number = 0;

	haveStore: boolean = false;
	store!: Store;
	private subscription: Subscription | undefined;
	constructor(
		private router: Router,
		private authService: AuthenticationService,
		private userService: UserService,
		private storeService: StoreService,
		private toastr: ToastrService,
		private hub: HubService
	) {
		const savedIndex = sessionStorage.getItem("selectedNavIndex");
		if (savedIndex !== null) {
			this.activeIndex = parseInt(savedIndex, 10);
		}
		setTimeout(() => {
			this.hub.startConnectionStoreByTable(
				"3d487deb-e1d1-489f-a266-c72fa02b1dc2"
			);
		}, 1000);

		this.hub.onReloadData((message) => {
			console.log(message);
		});
	}

	ngOnInit(): void {
		this.idUser = this.authService.getIdFromToken();
		this.loadStore();
	}

	ngOnDestroy(): void {
		if (this.subscription) {
			this.subscription.unsubscribe();
		}
	}

	loadStore(): void {
		const dataStore = sessionStorage.getItem("storeInfo");

		if (dataStore) {
			this.haveStore = true;
			this.store = JSON.parse(dataStore);
		} else {
			this.storeService.getByIdUser(this.idUser).subscribe({
				next: (res) => {
					if (res.isSuccess) {
						this.haveStore = true;
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
	}

	swithRoute(index: number) {
		this.activeIndex = index;
		this.statusNavbar = false;
		sessionStorage.setItem("selectedNavIndex", index.toString());
		switch (index) {
			case 1: {
				this.router.navigate(["/staff/table"]);
				break;
			}
			case 2: {
				this.router.navigate(["/staff/food"]);
				break;
			}
			case 3: {
				this.router.navigate(["/staff/order"]);
				break;
			}
			case 4: {
				this.router.navigate(["/staff/invoice"]);
				break;
			}
			case 5: {
				this.router.navigate([`/staff/${this.idUser}`]);
				break;
			}
		}
	}

	statusNavbar: boolean = false;
	openNavbar(): void {
		this.statusNavbar = !this.statusNavbar;
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

	logOut(): void {
		this.router.navigate(["/auth/login"]);
		localStorage.clear();
	}
}
