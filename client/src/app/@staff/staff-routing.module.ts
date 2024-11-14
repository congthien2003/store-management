import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TableComponent } from "./components/table/table.component";
import { StaffComponent } from "./staff.component";

const routes: Routes = [
	{
		path: "",
		component: StaffComponent,
		children: [
			{
				path: "table",
				component: TableComponent,
			},
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class StaffRoutingModule {}
