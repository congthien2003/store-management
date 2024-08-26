/// <reference types="@angular/localize" />

import { importProvidersFrom } from "@angular/core";
import { AppComponent } from "./app/app.component";
import { ToastrModule } from "ngx-toastr";
import { provideAnimations } from "@angular/platform-browser/animations";
import { CommonModule } from "@angular/common";
import { AppRoutingModule } from "./app/app-routing.module";
import { BrowserModule, bootstrapApplication } from "@angular/platform-browser";
import { LoadingInterceptor } from "./app/core/interceptors/loading.interceptor";
import { TokenInterceptor } from "./app/core/interceptors/token.interceptor";
import {
	HTTP_INTERCEPTORS,
	withInterceptorsFromDi,
	provideHttpClient,
} from "@angular/common/http";

bootstrapApplication(AppComponent, {
	providers: [
    importProvidersFrom(BrowserModule, AppRoutingModule, CommonModule, ToastrModule.forRoot({
        timeOut: 3000,
        positionClass: "toast-top-right",
        preventDuplicates: true,
    })),
    {
        provide: HTTP_INTERCEPTORS,
        useClass: TokenInterceptor,
        multi: true,
    },
    {
        provide: HTTP_INTERCEPTORS,
        useClass: LoadingInterceptor,
        multi: true,
    },
    provideAnimations(),
    provideHttpClient(withInterceptorsFromDi()),
    provideAnimations()
],
}).catch((err) => console.error(err));
