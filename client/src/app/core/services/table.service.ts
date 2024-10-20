import { Injectable } from '@angular/core';
import { TableApi } from '../constant/api/table.api';
import { MasterService } from './master/master.service';
import { Pagination } from '../models/common/Pagination';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/common/ApiResponse';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class TableService {
  endpoint = TableApi;
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
    return (
      this, this.service.get(`${this.endpoint.getAll}/${idStore}`, { params })
    );
  }
  getById(id: number): Observable<ApiResponse> {
    return this.service.get(`${this.endpoint.getById}/${id}`);
  }
  create(table: any): Observable<ApiResponse> {
    return this.service.post(`${this.endpoint.create}`, table);
  }
  update(table: any, id: number): Observable<ApiResponse> {
    return this.service.put(`${this.endpoint.update}/${id}`, table);
  }
  delete(id: number): Observable<ApiResponse> {
    return this.service.delete(`${this.endpoint.delete}/${id}`);
  }
}
