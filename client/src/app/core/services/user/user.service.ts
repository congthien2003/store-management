import { Injectable } from "@angular/core";
import { MasterService } from "../master/master.service";
import { UserApi } from "../../constant/api/user.api";
import { Observable } from "rxjs";
import { ApiResponse } from "../../models/common/ApiResponse";
import { Pagination } from "../../models/common/Pagination";
import { HttpParams } from "@angular/common/http";
import { UserResponse } from "../../models/responses/UserResponse";
import { HttpClientModule } from "@angular/common/http";

@Injectable({
	providedIn: "root",
})
export class UserService {
	endpoint = UserApi;
	constructor(private service: MasterService) {}

	list(
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
			.set("ascSort", ascSort);
		return this.service.get(this.endpoint.getAll, { params });
	}

	getById(id: number): Observable<ApiResponse> {
		return this.service.get(`${this.endpoint.getById}/${id}`);
	}

	create(user: any): Observable<ApiResponse> {
		return this.service.post(`${this.endpoint.create}`, user);
	}

	update(user: any): Observable<ApiResponse> {
		return this.service.put(`${this.endpoint.update}`, user);
	}

	deleteById(id: number): Observable<ApiResponse> {
		const params = new HttpParams().set("id", id);
		return this.service.delete(`${this.endpoint.delete}/`, { params });
	}
}
