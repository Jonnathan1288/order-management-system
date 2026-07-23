import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ShoppingCart } from '../entities';
import { OrderStatus } from '../types/order-status';
import { ApiResponse } from './api-response';

@Injectable({ providedIn: 'root' })
export class ShoppingCartApiService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.API_URL;

  create(shoppingCart: ShoppingCart): Observable<ShoppingCart> {
    return this.http
      .post<ApiResponse<ShoppingCart>>(`${this.apiUrl}/shopping-carts`, shoppingCart)
      .pipe(map((response) => response.data));
  }

  updateStatus(shoppingCartId: number, status: OrderStatus): Observable<ShoppingCart> {
    return this.http
      .patch<ApiResponse<ShoppingCart>>(
        `${this.apiUrl}/shopping-cart/${shoppingCartId}/status/${status}`,
        null
      )
      .pipe(map((response) => response.data));
  }
}
