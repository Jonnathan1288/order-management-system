import { CommonModule } from '@angular/common';
import { Component, computed, CUSTOM_ELEMENTS_SCHEMA, inject, input } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthSessionService } from '../../../core/auth-session.service';
import { Business } from '../../../data/entities';


@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  public business = input<Business | null>(null);

  private readonly session = inject(AuthSessionService);
  private readonly router = inject(Router);

  readonly isAuthenticated = computed(() => this.session.isAuthenticated());
  readonly isCustomer = computed(
    () => this.session.isAuthenticated() && this.session.claims().role === 'CUSTOMER'
  );
  readonly isAdmin = computed(
    () => this.session.isAuthenticated() && this.session.claims().role === 'ADMIN'
  );

  signOut(): void {
    this.session.clear();
    this.router.navigateByUrl('/home');
  }
}
