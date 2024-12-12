import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MasterService } from "../master/master.service";

@Injectable({
	providedIn: "root",
})
export class AiPredictService {
	endpoints = ["Client/RevenuePredict", "Client/GetPopularComboFoods"];
	constructor(private master: MasterService) {}

	predict(numdays: number) {
		return this.master.post(this.endpoints[0], { numdays });
	}

	getPopularCombos(idStore: number) {
		return this.master.post(this.endpoints[1], { idStore });
	}
}
