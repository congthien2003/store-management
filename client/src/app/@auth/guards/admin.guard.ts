import { Inject, inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthenticationService } from "../../core/services/auth/authentication.service";
import { map, catchError, Observable, of } from "rxjs";

export const adminGuard: CanActivateFn = (route, state) => {
	const service = inject(AuthenticationService);
	const router = inject(Router);
	let isAdmin = false;
	return service.checkToken().pipe(
		map((res) => {
			if (res.isSuccess && res.data.role === "0") {
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
