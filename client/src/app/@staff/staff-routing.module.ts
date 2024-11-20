import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TableComponent } from "./components/table/table.component";
import { StaffComponent } from "./staff.component";
import { FoodComponent } from "./components/food/food.component";
import { OrderComponent } from "./components/order/order.component";
import { InvoiceComponent } from "./components/invoice/invoice.component";
import { InforUserComponent } from "./components/infor-user/infor-user.component";

const routes: Routes = [
	{
		path: "",
		component: StaffComponent,
		children: [
			{
				path: "table",
				component: TableComponent,
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
				path: "infor-user",
				component: InforUserComponent
			},
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class StaffRoutingModule {}
