import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { AuthSessionService } from './auth-session.service';

export const apiInterceptor: HttpInterceptorFn = (request, next) => {
  const session = inject(AuthSessionService);
  const claims = session.claims();
  const headers: Record<string, string> = {
    'X-Business-Id': claims.businessId ?? String(environment.DEFAULT_BUSINESS_ID),
  };

  if (session.token) {
    headers['Authorization'] = `Bearer ${session.token}`;
  }

  if (claims.customerId) {
    headers['X-Customer-Id'] = claims.customerId;
  }
  console.log(headers)

  return next(request.clone({ setHeaders: headers }));
};
