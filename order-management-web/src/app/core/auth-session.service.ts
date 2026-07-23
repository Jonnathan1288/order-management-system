import { isPlatformBrowser } from '@angular/common';
import { inject, Injectable, PLATFORM_ID, signal } from '@angular/core';
import { UserRole } from '../data/types/role';

export interface SessionClaims {
  role: UserRole | null;
  businessId: string | null;
  customerId: string | null;
  email: string | null;
}

const TOKEN_KEY = 'token';
const ROLE_CLAIM =
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
const EMAIL_CLAIM =
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/email';

@Injectable({ providedIn: 'root' })
export class AuthSessionService {
  private readonly platformId = inject(PLATFORM_ID);
  private readonly tokenSignal = signal<string | null>(this.readStoredToken());

  get token(): string | null {
    return this.tokenSignal();
  }

  setToken(token: string): void {
    this.tokenSignal.set(token);
    if (this.isBrowser()) {
      localStorage.setItem(TOKEN_KEY, token);
    }
  }

  clear(): void {
    this.tokenSignal.set(null);
    if (this.isBrowser()) {
      localStorage.removeItem(TOKEN_KEY);
    }
  }

  isAuthenticated(): boolean {
    return !!this.token && !this.isExpired();
  }

  claims(): SessionClaims {
    const payload = this.decodePayload();
    const role = this.readRole(payload);

    return {
      role,
      businessId: this.readString(payload?.['businessId']),
      customerId: this.readString(payload?.['customerId']),
      email: this.readString(payload?.[EMAIL_CLAIM]),
    };
  }

  landingPath(): string {
    return this.claims().role === 'CUSTOMER' ? '/customer/orders' : '/admin';
  }

  private isExpired(): boolean {
    const exp = Number(this.decodePayload()?.['exp'] ?? 0);
    return exp > 0 && Date.now() >= exp * 1000;
  }

  private decodePayload(): Record<string, unknown> | null {
    const token = this.token;
    if (!token) return null;

    const [, payload] = token.split('.');
    if (!payload) return null;

    try {
      const normalized = payload.replace(/-/g, '+').replace(/_/g, '/');
      const decoded = atob(normalized.padEnd(Math.ceil(normalized.length / 4) * 4, '='));
      return JSON.parse(decoded) as Record<string, unknown>;
    } catch {
      return null;
    }
  }

  private readString(value: unknown): string | null {
    return typeof value === 'string' && value.trim() ? value : null;
  }

  private readRole(payload: Record<string, unknown> | null): UserRole | null {
    if (!payload) return null;

    const directRole = this.normalizeRole(
      payload[ROLE_CLAIM] ??
      payload['role']
    );
    if (directRole) return directRole;

    for (const value of Object.values(payload)) {
      const role = this.normalizeRole(value);
      if (role) return role;
    }

    return null;
  }

  private normalizeRole(value: unknown): UserRole | null {
    const values = Array.isArray(value) ? value : [value];

    for (const item of values) {
      const text = String(item ?? '').trim().toUpperCase();
      if (text.includes('CUSTOMER')) return 'CUSTOMER';
      if (text.includes('ADMIN')) return 'ADMIN';
    }

    return null;
  }

  private readStoredToken(): string | null {
    if (!this.isBrowser()) return null;
    return localStorage.getItem(TOKEN_KEY);
  }

  private isBrowser(): boolean {
    return isPlatformBrowser(this.platformId);
  }
}
