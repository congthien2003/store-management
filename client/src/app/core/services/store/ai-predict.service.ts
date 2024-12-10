import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
	providedIn: "root",
})
export class AiPredictService {
	endpoints = ["localhost:5000/predict", "localhost:5000/get_popular_combos"];
	constructor(private http: HttpClient) {}

	predict(numdays: number) {
		return this.http.post<any>(this.endpoints[0], { numdays });
	}
}
