import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Business } from '../entities';
import { ApiResponse } from './api-response';

@Injectable({ providedIn: 'root' })
export class BusinessApiService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.API_URL;

  getFirst(): Observable<Business> {
    return this.http
      .get<ApiResponse<Business>>(`${this.apiUrl}/business`)
      .pipe(map((response) => response.data));
  }
}
