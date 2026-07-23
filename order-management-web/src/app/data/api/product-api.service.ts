import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Product } from '../entities';
import { ApiResponse } from './api-response';

@Injectable({ providedIn: 'root' })
export class ProductApiService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.API_URL;

  list(): Observable<Product[]> {
    return this.http
      .get<ApiResponse<Product[]>>(`${this.apiUrl}/product`)
      .pipe(map((response) => response.data ?? []));
  }

  create(product: Product): Observable<Product> {
    return this.http
      .post<ApiResponse<Product>>(`${this.apiUrl}/product`, product)
      .pipe(map((response) => response.data));
  }

  listByCategory(categoryId: number): Observable<Product[]> {
    return this.http
      .get<ApiResponse<Product[]>>(`${this.apiUrl}/product/categories/${categoryId}`)
      .pipe(map((response) => response.data ?? []));
  }

  search(value: string): Observable<Product[]> {
    return this.http
      .get<ApiResponse<Product[]>>(`${this.apiUrl}/product/search`, {
        params: { value },
      })
      .pipe(map((response) => response.data ?? []));
  }
}
