import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiResponse } from "../../models/interfaces/Common/ApiResponse";
import { Pagination } from "../../models/interfaces/Common/Pagination";
import { MasterService } from "../master/master.service";
import { FoodApi } from "../../constant/api/food.api";

@Injectable({
	providedIn: "root",
})
export class FoodService {
	endpoint = FoodApi;
	constructor(private service: MasterService) {}

	list(
		idStore: number,
		pagi: Pagination,
		searchTerm: string = "",
		sortColumn: string = "",
		filter: boolean = false,
		ascSort: boolean = true,
		categoryId?: number
	): Observable<ApiResponse> {
		let params = new HttpParams()
			.set("idStore", idStore)
			.set("currentPage", pagi.currentPage)
			.set("pageSize", pagi.pageSize)
			.set("searchTerm", searchTerm)
			.set("sortColumn", sortColumn)
			.set("filter", filter)
			.set("ascSrot", ascSort)
			.set("categoryId", categoryId?.toString() ?? "");
		return this.service.get(this.endpoint.getByIdStore, { params });
	}

	listCombo(idStore: number): Observable<any> {
		return this.service.post(`${this.endpoint.listCombo}`, {
			idStore: idStore,
		});
	}

	getById(id: number): Observable<ApiResponse> {
		return this.service.get(`${this.endpoint.getById}/${id} `);
	}

	getByListId(listId: number[]): Observable<ApiResponse> {
		return this.service.post(`${this.endpoint.getByListId}`, listId);
	}

	getByCategory(
		idCategory: number,
		pagi: Pagination
	): Observable<ApiResponse> {
		const params = new HttpParams()
			.set("id", idCategory)
			.set("currentPage", pagi.currentPage)
			.set("pageSize", pagi.pageSize);
		return this.service.get(`${this.endpoint.getByIdCategory}`, { params });
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
