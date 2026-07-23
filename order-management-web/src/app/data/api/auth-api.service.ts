import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ApiResponse } from './api-response';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token?: string;
}

@Injectable({ providedIn: 'root' })
export class AuthApiService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.API_URL;

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http
      .post<ApiResponse<LoginResponse>>(`${this.apiUrl}/auth/login`, request)
      .pipe(map((response) => response.data));
  }
}
