import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PaymentMethod } from '../entities';
import { ApiResponse } from './api-response';

@Injectable({ providedIn: 'root' })
export class PaymentMethodApiService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.API_URL;

  list(): Observable<PaymentMethod[]> {
    return this.http
      .get<ApiResponse<PaymentMethod[]>>(`${this.apiUrl}/payment-method`)
      .pipe(map((response) => response.data ?? []));
  }
}
