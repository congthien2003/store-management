import { Injectable } from '@angular/core';
import { InvoiceApi } from '../constant/api/invoice.api';
import { MasterService } from './master/master.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/common/ApiResponse';
import { Pagination } from '../models/common/Pagination';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class InvoiceService {
  endpoint = InvoiceApi;
  constructor(private service: MasterService) {}

  list(
    idStore: number,
    pagi: Pagination,
    searchTerm: string = '',
    sortColumn: string = '',
    ascSort: boolean = true
  ): Observable<ApiResponse> {
    const params = new HttpParams()
      .set('idOrder', idStore)
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
  create(invoice: any): Observable<ApiResponse> {
    return this.service.post(`${this.endpoint.create}`, invoice);
  }
  update(id: number, invoice: any): Observable<ApiResponse> {
    return this.service.put(`${this.endpoint.update}/${id}`, invoice);
  }
}
