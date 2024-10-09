import { Injectable } from '@angular/core';
import { PaymentTypeApi } from '../../constant/api/paymentType.api';
import { MasterService } from '../master/master.service';
import { Pagination } from '../../models/common/Pagination';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/common/ApiResponse';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class PaymentTypeService {
  endpoint = PaymentTypeApi;
  constructor(private service: MasterService) {}
  list(
    idStore: number,
    pagi: Pagination,
    searchTerm: string = '',
    sortColumn: string = '',
    ascSort: boolean = true
  ): Observable<ApiResponse> {
    const params = new HttpParams()
      .set('idStore', idStore)
      .set('currentPage', pagi.currentPage)
      .set('pageSize', pagi.pageSize)
      .set('searchTerm', searchTerm)
      .set('sortColumn', sortColumn)
      .set('ascSrot', ascSort);
    return this.service.get(`${this.endpoint.getAll}/${idStore}`, params);
  }
  getById(id: number): Observable<ApiResponse> {
    return this.service.get(`${this.endpoint.getById}/${id}`);
  }
  create(order: any): Observable<ApiResponse> {
    return this.service.post(`${this.endpoint.create}`, order);
  }
  update(id: number, order: any): Observable<ApiResponse> {
    return this.service.put(`${this.endpoint.update}/${id}`, order);
  }
  delete(id: number): Observable<ApiResponse> {
    return this.service.delete(`${this.endpoint.delete}/${id}`);
  }
}
