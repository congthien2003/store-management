import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MasterService } from "../master/master.service";

@Injectable({
	providedIn: "root",
})
export class AiPredictService {
	endpoints = ["Client/GetPredictRevenue", "Client/GetPopularComboFoods"];
	constructor(private master: MasterService) {}

	predict(idStore: number) {
		return this.master.post(this.endpoints[0], { idStore });
	}

	getPopularCombos(idStore: number) {
		return this.master.post(this.endpoints[1], { idStore });
	}
}
