import { isPlatformBrowser } from '@angular/common';
import { computed, inject, Injectable, PLATFORM_ID, signal } from '@angular/core';
import { Product } from '../data/entities';

export interface ShoppingCartItem {
  product: Product;
  quantity: number;
}

const CART_KEY = 'shopping-cart';

@Injectable({ providedIn: 'root' })
export class ShoppingCartStateService {
  private readonly platformId = inject(PLATFORM_ID);
  private readonly itemsSignal = signal<ShoppingCartItem[]>(this.read());

  readonly items = this.itemsSignal.asReadonly();
  readonly totalItems = computed(() =>
    this.itemsSignal().reduce((total, item) => total + item.quantity, 0)
  );
  readonly total = computed(() =>
    this.itemsSignal().reduce(
      (total, item) => total + this.unitPrice(item.product) * item.quantity,
      0
    )
  );

  add(product: Product): void {
    const items = this.itemsSignal();
    const existing = items.find((item) => item.product.id === product.id);

    if (existing) {
      this.setItems(
        items.map((item) =>
          item.product.id === product.id
            ? { ...item, quantity: item.quantity + 1 }
            : item
        )
      );
      return;
    }

    this.setItems([...items, { product, quantity: 1 }]);
  }

  updateQuantity(productId: number, quantity: number): void {
    if (quantity <= 0) {
      this.remove(productId);
      return;
    }

    this.setItems(
      this.itemsSignal().map((item) =>
        item.product.id === productId ? { ...item, quantity } : item
      )
    );
  }

  remove(productId: number): void {
    this.setItems(this.itemsSignal().filter((item) => item.product.id !== productId));
  }

  clear(): void {
    this.setItems([]);
  }

  unitPrice(product: Product): number {
    const discount = product.discount ?? 0;
    return discount > 0 ? product.price - (product.price * discount) / 100 : product.price;
  }

  private setItems(items: ShoppingCartItem[]): void {
    this.itemsSignal.set(items);
    if (!this.isBrowser()) return;
    localStorage.setItem(CART_KEY, JSON.stringify(items));
  }

  private read(): ShoppingCartItem[] {
    if (!this.isBrowser()) return [];

    const value = localStorage.getItem(CART_KEY);
    if (!value) return [];

    try {
      const items = JSON.parse(value) as ShoppingCartItem[];
      return Array.isArray(items) ? items : [];
    } catch {
      return [];
    }
  }

  private isBrowser(): boolean {
    return isPlatformBrowser(this.platformId);
  }
}
