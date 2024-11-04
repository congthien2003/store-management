import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { map, catchError, of } from "rxjs";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";

export const ownerGuard: CanActivateFn = (route, state) => {
	const service = inject(AuthenticationService);
	const router = inject(Router);
	let isAdmin = false;
	return service.checkToken().pipe(
		map((res) => {
			if (res.isSuccess && res.data.role === "1") {
				return true;
			} else {
				router.navigate(["/not-authorized"]);
				return false;
			}
		}),
		catchError(() => {
			router.navigate(["/login"]);
			return of(false);
		})
	);
};
