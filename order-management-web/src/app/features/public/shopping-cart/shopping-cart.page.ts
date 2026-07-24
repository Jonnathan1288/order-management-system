import { CommonModule } from '@angular/common';
import { Component, computed, CUSTOM_ELEMENTS_SCHEMA, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthSessionService } from '../../../core/auth-session.service';
import { ShoppingCartStateService } from '../../../core/shopping-cart-state.service';
import { OrderApiService } from '../../../data/api/order-api.service';
import { PaymentMethodApiService } from '../../../data/api/payment-method-api.service';
import { PaymentMethod } from '../../../data/entities';

@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './shopping-cart.page.html',
  styleUrl: './shopping-cart.page.css',
})
export class ShoppingCartPage {
  readonly cart = inject(ShoppingCartStateService);
  private readonly session = inject(AuthSessionService);
  private readonly router = inject(Router);
  private readonly paymentMethodApi = inject(PaymentMethodApiService);
  private readonly orderApi = inject(OrderApiService);

  readonly confirmModalOpen = signal(false);
  readonly paymentMethods = signal<PaymentMethod[]>([]);
  readonly selectedPaymentMethodId = signal<number | null>(null);
  readonly deliveryAddress = signal('');
  readonly notes = signal('');
  readonly loadingPaymentMethods = signal(false);
  readonly creatingOrder = signal(false);
  readonly orderError = signal('');
  readonly orderSuccess = signal('');
  readonly isAuthenticated = computed(() => this.session.isAuthenticated());

  ngOnInit(): void {
    if (this.isAuthenticated()) {
      this.loadPaymentMethods();
    }
  }

  trackByProductId(_: number, item: { product: { id: number } }): number {
    return item.product.id;
  }

  trackByPaymentMethodId(_: number, item: { id: number }): number {
    return item.id;
  }

  handleCheckout(): void {
    if (!this.cart.items().length) return;

    if (!this.isAuthenticated()) {
      this.router.navigate(['/sign-in']);
      return;
    }

    this.orderError.set('');
    if (!this.selectedPaymentMethodId()) {
      this.orderError.set('Selecciona un metodo de pago.');
      return;
    }

    this.confirmModalOpen.set(true);
  }

  closeConfirmModal(): void {
    this.confirmModalOpen.set(false);
  }

  confirmPurchase(): void {
    const paymentMethodId = this.selectedPaymentMethodId();
    const deliveryAddress = this.deliveryAddress().trim();
    const notes = this.notes().trim();

    this.orderError.set('');
    this.orderSuccess.set('');

    if (!paymentMethodId) {
      this.orderError.set('Selecciona un metodo de pago.');
      return;
    }

    if (!deliveryAddress) {
      this.orderError.set('Ingresa la direccion de entrega.');
      return;
    }

    this.creatingOrder.set(true);
    this.orderApi
      .create({
        paymentMethodId,
        date: new Date().toISOString(),
        total: this.cart.total(),
        deliveryAddress,
        notes: notes || null,
        orderDetails: this.cart.items().map((item) => ({
          productId: item.product.id,
          quantity: item.quantity,
        })),
      })
      .pipe(finalize(() => this.creatingOrder.set(false)))
      .subscribe({
        next: () => {
          this.cart.clear();
          this.confirmModalOpen.set(false);
          this.deliveryAddress.set('');
          this.notes.set('');
          this.orderSuccess.set('Compra confirmada correctamente.');
        },
        error: () => {
          this.orderError.set('No se pudo confirmar la compra. Intenta nuevamente.');
        },
      });
  }

  private loadPaymentMethods(): void {
    this.loadingPaymentMethods.set(true);
    this.paymentMethodApi
      .list()
      .pipe(finalize(() => this.loadingPaymentMethods.set(false)))
      .subscribe({
        next: (paymentMethods) => {
          this.paymentMethods.set(paymentMethods ?? []);
          // this.selectedPaymentMethodId.set(paymentMethods?.[0]?.id ?? null);
        },
        error: () => {
          this.paymentMethods.set([]);
          this.orderError.set('No se pudieron cargar los metodos de pago.');
        },
      });
  }
}
