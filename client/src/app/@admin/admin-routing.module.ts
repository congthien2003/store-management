import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdminComponent } from "./admin.component";
import { AdminDashboardComponent } from "./components/admin-dashboard/admin-dashboard.component";
import { UserComponent } from "./components/management/user/user.component";
import { StoreComponent } from "./components/management/store/store.component";
import { OwnerComponent } from "./components/management/owner/owner.component";
import { authGuard } from "../@auth/guards/auth.guard";
import { adminGuard } from "../@auth/guards/admin.guard";
const routes: Routes = [
	{
		path: "",
		component: AdminComponent,
		canActivate: [authGuard, adminGuard],
		children: [
			{
				path: "dashboard",
				component: AdminDashboardComponent,
			},
			{
				path: "user",
				component: UserComponent,
			},
			{
				path: "owner",
				component: OwnerComponent,
			},
			{
				path: "store",
				component: StoreComponent,
			},
		],
	},
	{ path: "", redirectTo: "admin", pathMatch: "full" },
	{ path: "**", redirectTo: "admin" },
];
@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class AdminRoutingModule {}
