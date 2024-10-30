import { Injectable } from "@angular/core";
import { FoodApi } from "../../constant/api/food.api";
import { MasterService } from "../master/master.service";
import { Pagination } from "../../models/common/Pagination";
import { Observable } from "rxjs";
import { ApiResponse } from "../../models/common/ApiResponse";
import { HttpParams } from "@angular/common/http";

@Injectable({
	providedIn: "root",
})
export class FoodService {
	endpoint = FoodApi;
	constructor(private service: MasterService) {}

	list(
		idCategory: number,
		pagi: Pagination,
		searchTerm: string = "",
		sortColumn: string = "",
		ascSort: boolean = true
	): Observable<ApiResponse> {
		const params = new HttpParams()
			.set("idCategory", idCategory)
			.set("currentPage", pagi.currentPage)
			.set("pageSize", pagi.pageSize)
			.set("searchTerm", searchTerm)
			.set("sortColumn", sortColumn)
			.set("ascSrot", ascSort);
		return this.service.get(this.endpoint.getAll, { params });
	}

	getById(id: number): Observable<ApiResponse> {
		return this.service.get(`${this.endpoint.getById}/${id}`);
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

	create(food: any): Observable<ApiResponse> {
		return this.service.post(`${this.endpoint.create}`, food);
	}
	update(food: any, id: number): Observable<ApiResponse> {
		return this.service.put(`${this.endpoint.update}/${id}`, food);
	}
	delete(id: number): Observable<ApiResponse> {
		return this.service.delete(`${this.endpoint.delete}/${id}`);
	}
	getByIdStore(
		idStore: number,
		pagi: Pagination,
		searchTerm: string = "",
		sortColumn: string = "",
		ascSort: boolean = true
	): Observable<ApiResponse> {
		const params = new HttpParams()

			.set("currentPage", pagi.currentPage)
			.set("pageSize", pagi.pageSize)
			.set("searchTerm", searchTerm)
			.set("sortColumn", sortColumn)
			.set("ascSrot", ascSort);
		return this.service.get(`${this.endpoint.getAllByIdStore}/${idStore}`, {
			params,
		});
	}
}
