import { Injectable } from '@angular/core';
import { MasterService } from '../master/master.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/common/ApiResponse';
import { Pagination } from '../../models/common/Pagination';
import { HttpParams } from '@angular/common/http';
import { StoreApi } from '../../constant/api/store.api';

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
  deleteById(id: number): Observable<ApiResponse> {
    const params = new HttpParams().set('id', id);
    return this.service.delete(`${this.endpoint.delete}/`, { params });
  }
  getByIdUser(idUser: number): Observable<ApiResponse> {
    return this.service.get(`${this.endpoint.getByIdUser}/${idUser}`);
  }
}
