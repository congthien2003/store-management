import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { OwnerComponent } from "./owner.component";
import { MyStoreComponent } from "./components/management/my-store/my-store.component";
import { CategoryComponent } from "./components/management/category/category.component";
import { FoodComponent } from "./components/management/food/food.component";
import { OrderComponent } from "./components/management/order/order.component";
import { InvoiceComponent } from "./components/management/invoice/invoice.component";
import { StaffComponent } from "./components/management/staff/staff.component";
import { AnalyticsComponent } from "./components/management/analytics/analytics.component";
import { OwnerDashboardComponent } from "./components/owner-dashboard/owner-dashboard.component";
import { TableComponent } from "./components/management/table/table.component";
import { ownerGuard } from "../@auth/guards/owner.guard";
import { InforUserComponent } from "./components/management/infor-user/infor-user.component";

const routes: Routes = [
	{
		path: "",
		component: OwnerComponent,
		canActivate: [ownerGuard],
		children: [
			{
				path: "dashboard",
				component: OwnerDashboardComponent,
			},
			{
				path: "my-store",
				component: MyStoreComponent,
			},
			{
				path: "table",
				component: TableComponent,
			},
			{
				path: "category",
				component: CategoryComponent,
			},
			{
				path: "food",
				component: FoodComponent,
			},
			{
				path: "order",
				component: OrderComponent,
			},
			{
				path: "invoice",
				component: InvoiceComponent,
			},
			{
				path: "staff",
				component: StaffComponent,
			},
			{
				path: "analytics",
				component: AnalyticsComponent,
			},
			{
				path: "infor-user",
				component: InforUserComponent,
			},
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class OwnerRoutingModule {}
