import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserRole } from '../data/types/role';
import { AuthSessionService } from './auth-session.service';

export const authGuard: CanActivateFn = () => {
  const session = inject(AuthSessionService);
  const router = inject(Router);

  if (session.isAuthenticated()) return true;

  return router.createUrlTree(['/sign-in']);
};

export const roleGuard = (roles: UserRole[]): CanActivateFn => {
  return () => {
    const session = inject(AuthSessionService);
    const router = inject(Router);
    const role = session.claims().role;

    if (session.isAuthenticated() && role && roles.includes(role)) return true;
    if (session.isAuthenticated()) return router.createUrlTree([session.landingPath()]);

    return router.createUrlTree(['/sign-in']);
  };
};
