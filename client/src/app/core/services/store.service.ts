import { Injectable } from '@angular/core';
import { StoreApi } from '../constant/api/store.api';
import { MasterService } from './master/master.service';
import { Pagination } from '../models/common/Pagination';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/common/ApiResponse';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class StoreService {
  endpoint = StoreApi;
  constructor(private service: MasterService) {}

  list(
    pagi: Pagination,
    searchTerm: string = '',
    sortColumn: string = '',
    ascSort: boolean = true
  ): Observable<ApiResponse> {
    const params = new HttpParams()
      .set('currentPage', pagi.currentPage)
      .set('pageSize', pagi.pageSize)
      .set('searchTerm', searchTerm)
      .set('sortColumn', sortColumn)
      .set('ascSrot', ascSort);
    return this.service.get(this.endpoint.getAll, { params });
  }

  getById(id: number): Observable<ApiResponse> {
    return this.service.get(`${this.endpoint.getById}/${id}`);
  }
  create(store: any): Observable<ApiResponse> {
    return this.service.post(`${this.endpoint.create}`, store);
  }
  update(store: any, id: number): Observable<ApiResponse> {
    return this.service.put(`${this.endpoint.update}/${id}`, store);
  }
  getByIdUser(idUser: number): Observable<ApiResponse> {
    return this.service.get(`${this.endpoint.getByIdUser}/${idUser}`);
  }
}
