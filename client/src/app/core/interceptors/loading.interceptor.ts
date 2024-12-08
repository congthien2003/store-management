import { Injectable } from "@angular/core";
import {
	HttpRequest,
	HttpHandler,
	HttpEvent,
	HttpInterceptor,
} from "@angular/common/http";
import { Observable } from "rxjs";
import { finalize } from "rxjs/operators";
import { LoaderService } from "../services/loader.service";

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
	private totalRequests = 0;
	// Mảng các URL cần bỏ qua loading
	private ignoreUrls: string[] = [
		"https://localhost:7272/api/client/chatGemini",
		// Thêm các URL khác nếu cần
	];
	constructor(private loadingService: LoaderService) {}

	intercept(
		request: HttpRequest<unknown>,
		next: HttpHandler
	): Observable<HttpEvent<unknown>> {
		this.totalRequests++;
		// Kiểm tra xem URL có nằm trong mảng ignoreUrls không
		if (this.ignoreUrls.includes(request.url)) {
			return next.handle(request);
		}
		this.loadingService.setLoading(true);
		return next.handle(request).pipe(
			finalize(() => {
				this.totalRequests--;
				if (this.totalRequests == 0) {
					this.loadingService.setLoading(false);
				}
			})
		);
	}
}
