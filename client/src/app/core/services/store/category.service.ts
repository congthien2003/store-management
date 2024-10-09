import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pagination } from '../../models/common/Pagination';
import { CategoryApi } from '../../constant/api/category.api';
import { ApiResponse } from '../../models/common/ApiResponse';
import { HttpParams } from '@angular/common/http';
import { MasterService } from '../master/master.service';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  endpoint = CategoryApi;
  constructor(private service: MasterService) {}

  list(
    idStore: number,
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
    return this.service.get(`${this.endpoint.getAll}/${idStore}`, { params });
  }

  getById(id: number): Observable<ApiResponse> {
    return this.service.get(`${this.endpoint.getById}/${id}`);
  }
  create(catgory: any): Observable<ApiResponse> {
    return this.service.post(`${this.endpoint.create}`, catgory);
  }
  update(category: any, id: number): Observable<ApiResponse> {
    return this.service.put(`${this.endpoint.update}/${id}`, category);
  }
  delete(id: number): Observable<ApiResponse> {
    return this.service.delete(`${this.endpoint.delete}/${id}`);
  }
}
