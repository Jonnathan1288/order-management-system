import { CommonModule } from '@angular/common';
import { Component, computed, CUSTOM_ELEMENTS_SCHEMA, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthSessionService } from '../../../core/auth-session.service';
import { ShoppingCartStateService } from '../../../core/shopping-cart-state.service';

@Component({
  selector: 'app-floating-cart-button',
  standalone: true,
  imports: [CommonModule, RouterLink],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './floating-cart-button.component.html',
  styleUrl: './floating-cart-button.component.css',
})
export class FloatingCartButtonComponent {
  private readonly session = inject(AuthSessionService);
  readonly cart = inject(ShoppingCartStateService);

  readonly canUseCart = computed(
    () => !(this.session.isAuthenticated() && this.session.claims().role === 'ADMIN')
  );
}
