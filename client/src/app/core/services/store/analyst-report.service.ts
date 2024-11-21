import { Injectable } from '@angular/core';
import { AnalystReportApi } from '../../constant/api/analyst-report.api';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/interfaces/Common/ApiResponse';
import { MasterService } from '../master/master.service';

@Injectable({
  providedIn: 'root',
})
export class AnalystReportService {
  endpoint = AnalystReportApi;
  constructor(private service: MasterService) {}

  getCountFood(idStore: number, dateTime: string): Observable<ApiResponse> {
    const params = new HttpParams().set('dateTime', dateTime);
    return this.service.get(`${this.endpoint.countFood}/${idStore}`, {
      params,
    });
  }

  getCountOrder(idStore: number, dateTime: string): Observable<ApiResponse> {
    const params = new HttpParams().set('dateTime', dateTime);
    return this.service.get(`${this.endpoint.countOrder}/${idStore}`, {
      params,
    });
  }
  getTableFree(idStore: number): Observable<ApiResponse> {
    return this.service.get(`${this.endpoint.tableFree}/${idStore}`);
  }
  getDailyRevenue(idStore: number, dateTime: string): Observable<ApiResponse> {
    return this.service.get(
      `${this.endpoint.dailyRevenue}/${idStore}?dateTime=${dateTime}`
    );
  }
  getOrderMonth(idStore: number, year: number): Observable<ApiResponse> {
    return this.service.get(`${this.endpoint.monthOrder}/${idStore}/${year}`);
  }
  getRevenueMonth(idStore: number, year: number): Observable<ApiResponse> {
    return this.service.get(`${this.endpoint.monthRevenue}/${idStore}/${year}`);
  }
}
