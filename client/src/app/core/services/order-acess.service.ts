import { Injectable } from "@angular/core";
import { OrderAccessAPI } from "../constant/api/orderaccess.api";
import { HttpParams } from "@angular/common/http";
import { OrderAccessToken } from "../models/interfaces/OrderAccessToken";
import { Observable } from "rxjs";
import { ApiResponse } from "../models/common/ApiResponse";
import { MasterService } from "./master/master.service";

@Injectable({
	providedIn: "root",
})
export class OrderAcessService {
	endpoint = OrderAccessAPI;
	constructor(private service: MasterService) {}

	getByURL(idTable: string, idStore: string): Observable<ApiResponse> {
		const params = new HttpParams()
			.set("idTable", idTable)
			.set("idStore", idStore);
		return this.service.get(`${this.endpoint.ByURL}`, { params });
	}

	create(data: OrderAccessToken): Observable<ApiResponse> {
		return this.service.post(`${this.endpoint.ByID}`, data);
	}

	update(id: string, data: OrderAccessToken): Observable<ApiResponse> {
		return this.service.put(`${this.endpoint.ByID}/${id}`, data);
	}

	deleteById(id: string): Observable<ApiResponse> {
		return this.service.delete(`${this.endpoint.ByID}/${id}`);
	}

	request(idTable: string, idStore: string): Observable<ApiResponse> {
		const params = new HttpParams()
			.set("idTable", idTable)
			.set("idStore", idStore);
		return this.service.get(`${this.endpoint.ByID}/request`, { params });
	}
}
