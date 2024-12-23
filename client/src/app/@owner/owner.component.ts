import {
	Component,
	effect,
	ElementRef,
	OnDestroy,
	OnInit,
	signal,
	ViewChild,
} from "@angular/core";
import { CommonModule } from "@angular/common";
import { MatButtonModule } from "@angular/material/button";
import { MatCommonModule } from "@angular/material/core";
import { MatMenuModule } from "@angular/material/menu";
import { Router, RouterOutlet } from "@angular/router";
import { NotfoundStoreComponent } from "./components/notfound-store/notfound-store.component";
import { AuthenticationService } from "../core/services/auth/authentication.service";
import { UserService } from "../core/services/user/user.service";
import { StoreService } from "../core/services/store/store.service";
import { ToastrService } from "ngx-toastr";
import { SpinnerComponent } from "../shared/components/spinner/spinner.component";
import { HubService } from "../core/services/hubStore.service";
import { Store } from "../core/models/interfaces/Store";
import { OrderAccessToken } from "../core/models/interfaces/OrderAccessToken";
import { Subscription } from "rxjs";
import { BankInfoService } from "../core/services/store/bank-info.service";
import { FormsModule } from "@angular/forms";
import { GeminiService } from "../core/services/third-party/gemini.service";
import { FormatTextPipe } from "../core/utils/format-text.pipe";
import { LoaderService } from "../core/services/loader.service";
import { NotificationClientService } from "../core/services/store/notification-client.service";
const MatModuleImport = [MatButtonModule, MatCommonModule, MatMenuModule];

@Component({
	selector: "app-owner",
	standalone: true,
	imports: [
		CommonModule,
		MatModuleImport,
		RouterOutlet,
		NotfoundStoreComponent,
		SpinnerComponent,
		FormatTextPipe,
	],
	templateUrl: "./owner.component.html",
	styleUrls: ["./owner.component.scss"],
})
export class OwnerComponent implements OnInit, OnDestroy {
	currentURL: string = "dashboard";
	activeIndex: number = 0;
	idUser: number = 0;

	haveStore: boolean = false;
	store!: Store;
	private subscription: Subscription | undefined;
	showChatBox: boolean = false;
	message: any[] = [
		{
			text: "Hello Owner",
			timeline: "11:00 AM",
			role: 0, // Gemini
		},
		{
			text: "Hello Gemini",
			timeline: "11:11 AM",
			role: 1, // Owner
		},
	];
	messageText: string = "";
	notifications: any[] = [];
	notificationCount: number = 0;
	showNotifi: boolean = false;
	constructor(
		private router: Router,
		private authService: AuthenticationService,
		private userService: UserService,
		private storeService: StoreService,
		private toastr: ToastrService,
		private hub: HubService,
		private geminiService: GeminiService,
		private loader: LoaderService,
		private notificationService: NotificationClientService
	) {
		const savedIndex = sessionStorage.getItem("selectedNavIndex");
		if (savedIndex !== null) {
			this.activeIndex = parseInt(savedIndex, 10);
		}
		this.hub.onReloadData((message) => {
			console.log(message);
		});

		this.notifications = this.notificationService.loadList();

		effect(() => {
			this.notificationCount =
				this.notificationService.notificationCount();
			this.notifications = this.notificationService.loadList();
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
			case 0: {
				this.router.navigate(["/owner/dashboard"]);
				break;
			}
			case 1: {
				this.router.navigate(["/owner/my-store"]);
				break;
			}
			case 2: {
				this.router.navigate(["/owner/table"]);
				break;
			}
			case 3: {
				this.router.navigate(["/owner/category"]);
				break;
			}
			case 4: {
				this.router.navigate(["/owner/food"]);
				break;
			}
			case 5: {
				this.router.navigate(["/owner/order"]);
				break;
			}
			case 6: {
				this.router.navigate(["/owner/invoice"]);
				break;
			}
			case 7: {
				this.router.navigate(["/owner/staff"]);
				break;
			}
			case 8: {
				this.router.navigate(["/owner/analytics"]);
				break;
			}
			case 9: {
				this.router.navigate(["/owner/infor-user"]);
				break;
			}
			case 11: {
				this.router.navigate(["/owner/combos"]);
				break;
			}
			case 12: {
				this.router.navigate(["/owner/predict-revenue"]);
				break;
			}
			case 13: {
				this.router.navigate(["/owner/my-ticket"]);
				break;
			}
			case 14: {
				this.router.navigate(["/owner/staff"]);
				break;
			}
		}
	}
	statusNavbar: boolean = false;
	openNavbar(): void {
		this.statusNavbar = !this.statusNavbar;
	}

	openChatBox(): void {
		this.showChatBox = !this.showChatBox;
	}
	loadingMessage: boolean = false;
	@ViewChild("scrollContainer") private scrollContainer!: ElementRef;
	submitChatBox(): void {
		console.log("Submit Chat: " + this.messageText);
		this.loadingMessage = true;
		this.loader.setLoading(false);
		this.scrollToBottom();
		this.message.push({
			text: this.messageText,
			timeline: new Date().toLocaleTimeString(),
			role: 1, // Owner
		});
		this.geminiService.chat(this.messageText).subscribe({
			next: (res) => {
				if (res.isSuccess) {
					this.message.push({
						text: res.data,
						timeline: new Date().toLocaleTimeString(),
						role: 0, // Gemini
					});
					this.scrollToBottom();
					this.loadingMessage = false;
					this.geminiService.saveChatHistory(this.message);
				} else {
					this.toastr.success(res.message, "Thông báo", {
						timeOut: 3000,
					});
				}
				this.messageText = "";
			},
		});
	}

	// Hàm tự động scroll xuống
	private scrollToBottom(): void {
		try {
			this.scrollContainer.nativeElement.scrollTop =
				this.scrollContainer.nativeElement.scrollHeight;
		} catch (err) {
			console.error(err);
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

	logOut(): void {
		this.router.navigate(["/auth/login"]);
		localStorage.clear();
	}
}
