import { Injectable } from "@angular/core";
import { BankInfoApi } from "../../constant/api/bankinfo.api";
import { HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { ApiResponse } from "../../models/interfaces/Common/ApiResponse";
import { Pagination } from "../../models/interfaces/Common/Pagination";
import { MasterService } from "../master/master.service";
import { BankInfo } from "../../models/interfaces/BankInfo";

@Injectable({
	providedIn: "root",
})
export class BankInfoService {
	endpoint = BankInfoApi;
	constructor(private service: MasterService) {}

	getAllByIdStore(idStore: number): Observable<ApiResponse> {
		return this.service.get(`${this.endpoint.getByIdStore}/${idStore}`);
	}

	getById(id: number): Observable<ApiResponse> {
		return this.service.get(`${this.endpoint.getById}/${id} `);
	}

	create(data: any): Observable<ApiResponse> {
		return this.service.post(`${this.endpoint.create}`, data);
	}

	update(id: number, data: any): Observable<ApiResponse> {
		return this.service.put(`${this.endpoint.update}/${id}`, data);
	}

	deleteById(id: number): Observable<ApiResponse> {
		const params = new HttpParams().set("id", id);
		return this.service.delete(`${this.endpoint.delete}/` + id);
	}

	generateQR(bankinfo: BankInfo, amount: number = 0): string {
		const qrUrl = `https://img.vietqr.io/image/${bankinfo.bankId}-${
			bankinfo.accountId
		}-compact.png?amount=${amount}&accountName=${encodeURIComponent(
			bankinfo.accountName
		)}`;
		return qrUrl;
	}
}
