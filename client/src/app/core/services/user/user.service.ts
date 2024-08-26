import { Injectable } from "@angular/core";
import { MasterService } from "../master/master.service";
import { UserApi } from "../../constant/api/user.api";
import { Observable } from "rxjs";
import { ApiResponse } from "../../models/interfaces/ApiResponse";

@Injectable({
	providedIn: "root",
})
export class UserService {
	endpoint = UserApi;
	constructor(private service: MasterService) {}

	list(): Observable<ApiResponse> {
		return this.service.get(this.endpoint.getAll);
	}

	getById(id: number): Observable<ApiResponse> {
		return this.service.get(`${this.endpoint.getById}/${id} `);
	}

	create(user: any): Observable<ApiResponse> {
		return this.service.post(`${this.endpoint.create}`, user);
	}

	update(user: any): Observable<ApiResponse> {
		return this.service.put(`${this.endpoint.update}`, user);
	}
}
