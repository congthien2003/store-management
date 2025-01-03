import { trigger, transition, style, animate } from "@angular/animations";
import { HttpClient, HttpClientModule } from "@angular/common/http";
import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { NZ_I18N, en_US } from "ng-zorro-antd/i18n";

@Component({
	selector: "app-root",
	template: " <router-outlet></router-outlet> ",
	standalone: true,
	imports: [RouterOutlet],
	providers: [{ provide: NZ_I18N, useValue: en_US }],
})
export class AppComponent {}
