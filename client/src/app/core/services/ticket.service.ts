import { Injectable } from '@angular/core';

import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/interfaces/Common/ApiResponse';
import { Pagination } from '../models/interfaces/Common/Pagination';
import { MasterService } from './master/master.service';
import { TicketApi } from '../constant/api/ticket.api';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  endpoint = TicketApi;
  constructor(private service: MasterService) {}

  create(ticket: any): Observable<ApiResponse> {
    return this.service.post(`${this.endpoint.create}`, ticket);
  }
  update(id: number, ticket: any): Observable<ApiResponse> {
    return this.service.put(`${this.endpoint.update}/${id}`, ticket);
  }
  delete(id: number): Observable<ApiResponse> {
    return this.service.delete(`${this.endpoint.delete}/${id}`);
  }
  getById(id: number): Observable<ApiResponse> {
    return this.service.get(`${this.endpoint.getById}/${id}`);
  }
  getAll(
    status: number,
    pagi: Pagination,
    ascSort: boolean = false,
    filter: boolean = false
  ): Observable<ApiResponse> {
    const params = new HttpParams()
      .set('currentPage', pagi.currentPage.toString())
      .set('pageSize', pagi.pageSize.toString())
      .set('asc', ascSort.toString())
      .set('filter', filter.toString());
    return this.service.get(`${this.endpoint.getAll}/${status}`, { params });
  }
  getMyTicket(
    idUser: number,
    status: number,
    pagi: Pagination,
    asc: boolean = false,
    filter: boolean = false
  ): Observable<ApiResponse> {
    const params = new HttpParams()
      .set('currentPage', pagi.currentPage.toString())
      .set('pageSize', pagi.pageSize.toString())
      .set('asc', asc.toString())
      .set('filter', filter.toString());
    return this.service.get(
      `${this.endpoint.getMyTicket}/${idUser}/${status}`,
      {
        params,
      }
    );
  }
}
