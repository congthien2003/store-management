import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Pagination } from "../../models/interfaces/Common/Pagination";

@Injectable({
	providedIn: "root",
})
export class StaffService {
	private baseUrl = `${environment.apiUrl}/staff`;

	constructor(private http: HttpClient) {}

	create(staff: any): Observable<any> {
		return this.http.post(`${this.baseUrl}/create`, staff);
	}

	getByStore(
		storeId: number,
		pagination: Pagination,
		searchTerm: string = "",
		filter: boolean = false
	): Observable<any> {
		let params = new HttpParams()
			.set("currentPage", pagination.currentPage.toString())
			.set("pageSize", pagination.pageSize.toString())
			.set("searchTerm", searchTerm)
			.set("filter", filter.toString());

		return this.http.get(`${this.baseUrl}/all/${storeId}`, { params });
	}

	update(staff: any): Observable<any> {
		return this.http.put(`${this.baseUrl}/update`, staff);
	}

	delete(id: number): Observable<any> {
		return this.http.delete(`${this.baseUrl}/${id}`);
	}
}
