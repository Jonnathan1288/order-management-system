import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Order } from '../entities';
import { ApiResponse } from './api-response';

@Injectable({ providedIn: 'root' })
export class OrderApiService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.API_URL;

  create(order: Order): Observable<Order> {
    return this.http
      .post<ApiResponse<Order>>(`${this.apiUrl}/order`, order)
      .pipe(map((response) => response.data));
  }

  list(): Observable<Order[]> {
    return this.http
      .get<ApiResponse<Order[]>>(`${this.apiUrl}/order`)
      .pipe(map((response) => response.data ?? []));
  }

  findCustomerOrder(orderId: number): Observable<Order | null> {
    return this.http
      .get<ApiResponse<Order | null>>(`${this.apiUrl}/order/${orderId}/customer`)
      .pipe(map((response) => response.data));
  }
}
