import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Category } from '../entities';
import { ApiResponse } from './api-response';


@Injectable({ providedIn: 'root' })
export class CategoryApiService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.API_URL;

  list(): Observable<Category[]> {
    return this.http
      .get<ApiResponse<Category[]>>(`${this.apiUrl}/category`)
      .pipe(map((response) => response.data ?? []));
  }

  create(category: Category): Observable<Category> {
    return this.http
      .post<ApiResponse<Category>>(`${this.apiUrl}/category`, category)
      .pipe(map((response) => response.data));
  }

  listByParent(parentId: number | null = null): Observable<Category[]> {
    const value = parentId === null ? '' : String(parentId);

    return this.http
      .get<ApiResponse<Category[]>>(`${this.apiUrl}/category/parent`, {
        params: { parentId: value },
      })
      .pipe(map((response) => response.data ?? []));
  }
}
