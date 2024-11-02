import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NotAuthorComponent } from "./shared/components/not-author/not-author.component";
import { AccessDeninedComponent } from "./shared/components/access-denined/access-denined.component";

const routes: Routes = [
	{
		path: "auth",
		loadChildren: () =>
			import("./@auth/auth.module").then((m) => m.AuthModule),
	},
	{
		path: "pages",
		loadChildren: () =>
			import("./pages/pages.module").then((m) => m.PagesModule),
	},
	{
		path: "admin",
		loadChildren: () =>
			import("./@admin/admin.module").then((m) => m.AdminModule),
	},
	{
		path: "owner",
		loadChildren: () =>
			import("./@owner/owner.module").then((m) => m.OwnerModule),
	},
	{
		path: "",
		loadChildren: () =>
			import("./@customer/customer.module").then((m) => m.CustomerModule),
	},
	{
		path: "not-authorized",
		component: NotAuthorComponent,
	},
	{
		path: "denied",
		component: AccessDeninedComponent,
	},
	{ path: "", redirectTo: "pages", pathMatch: "full" },
	{ path: "**", redirectTo: "pages" },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
