import { Injectable } from "@angular/core";
import { MasterService } from "../master/master.service";
import { ComboAPI } from "../../constant/api/combo.api";
import { ApiResponse } from "../../models/interfaces/Common/ApiResponse";
import { Observable } from "rxjs";
import { HttpParams } from "@angular/common/http";
import { Pagination } from "../../models/interfaces/Common/Pagination";

@Injectable({
	providedIn: "root",
})
export class ComboService {
	endpoint = ComboAPI;
	constructor(private service: MasterService) {}

	getAllByStore(
		idStore: number,
		pagi: Pagination,
		searchTerm: string,
		sortColumn: string
	): Observable<ApiResponse> {
		const params = new HttpParams()
			.set("idStore", idStore)
			.set("currentPage", pagi.currentPage)
			.set("pageSize", pagi.pageSize)
			.set("searchTerm", searchTerm)
			.set("sortColumn", sortColumn);
		return this.service.get(`${this.endpoint.getAllByIdStore}`, {
			params,
		});
	}

	getById(id: number): Observable<ApiResponse> {
		return this.service.get(`${this.endpoint.getById}/${id} `);
	}

	create(user: any): Observable<ApiResponse> {
		return this.service.post(`${this.endpoint.create}`, user);
	}

	update(id: number, user: any): Observable<ApiResponse> {
		return this.service.put(`${this.endpoint.update}/${id}`, user);
	}

	deleteById(id: number): Observable<ApiResponse> {
		return this.service.delete(`${this.endpoint.delete}/${id}`);
	}
}
