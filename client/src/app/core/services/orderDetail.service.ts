import { Injectable } from '@angular/core';
import { OrderDetailApi } from '../constant/api/orderDetail.api';
import { MasterService } from './master/master.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/common/ApiResponse';
import { Pagination } from '../models/common/Pagination';
import { HttpParams } from '@angular/common/http';
@Injectable({
  providedIn: 'root',
})
export class OrderDetailService {
  endpoint = OrderDetailApi;
  constructor(private service: MasterService) {}
  list(
    idOrder: number,
    pagi: Pagination,
    searchTerm: string = '',
    sortColumn: string = '',
    ascSort: boolean = true
  ): Observable<ApiResponse> {
    const params = new HttpParams()
      .set('idOrder', idOrder)
      .set('pageSize', pagi.pageSize)
      .set('searchTerm', searchTerm)
      .set('sortColumn', sortColumn)
      .set('ascSrot', ascSort);
    return this.service.get(`${this.endpoint.getAll}/${idOrder}`, params);
  }
  update(orderDetail: any): Observable<ApiResponse> {
    return this.service.put(`${this.endpoint.update}`, orderDetail);
  }
  delete(idOrder: number, idFood: number): Observable<ApiResponse> {
    return this.service.delete(`${this.endpoint.delete}/${idOrder}/${idFood}`);
  }
}
