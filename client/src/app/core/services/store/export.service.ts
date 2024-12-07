import { Injectable } from "@angular/core";
import { MasterService } from "../master/master.service";
import { HttpParams } from "@angular/common/http";
import { Observable } from "rxjs/internal/Observable";

@Injectable({
	providedIn: "root",
})
export class ExportService {
	endpoints = ["Sales/export-excel"];
	constructor(private master: MasterService) {}

	exportExcel(
		idStore: number,
		startDate: string,
		endDate: string
	): Observable<any> {
		return this.master.get(
			`${this.endpoints[0]}?idStore=${idStore}&startDateStr=${startDate}&endDateStr=${endDate}`,
			{ responseType: "blob" }
		);
	}
}
