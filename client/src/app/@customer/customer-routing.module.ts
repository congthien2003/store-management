import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { OrderComponent } from "./components/order/order.component";
import { DeniedPagesComponent } from "./components/denied-pages/denied-pages.component";
import { CartComponent } from "./components/cart/cart.component";
const routes: Routes = [
	{
		path: ":idStore/order/:idTable",
		component: OrderComponent,
	},
	{
		path: ":idStore/cart/:idTable",
		component: CartComponent,
	},
	{
		path: "order/denied",
		component: DeniedPagesComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class CustomerRoutingModule {}
