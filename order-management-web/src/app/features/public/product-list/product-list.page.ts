import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, PLATFORM_ID, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, finalize } from 'rxjs';
import { AuthSessionService } from '../../../core/auth-session.service';
import { ShoppingCartStateService } from '../../../core/shopping-cart-state.service';
import { CategoryApiService } from '../../../data/api/category-api.service';
import { ProductApiService } from '../../../data/api/product-api.service';
import { Category, Product } from '../../../data/entities';

@Component({
  selector: 'app-product-list-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './product-list.page.html',
  styleUrl: './product-list.page.css',
})
export class ProductListPage {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly categoryApi = inject(CategoryApiService);
  private readonly productApi = inject(ProductApiService);
  private readonly session = inject(AuthSessionService);
  private readonly cart = inject(ShoppingCartStateService);
  private readonly platformId = inject(PLATFORM_ID);

  readonly loading = signal(true);
  readonly error = signal('');
  readonly categories = signal<Category[]>([]);
  readonly products = signal<Product[]>([]);
  readonly selectedCategoryId = signal<number | null>(null);
  readonly productSearch = signal('');

  readonly activeProducts = computed(() =>
    this.products().filter((product) => product.active)
  );

  readonly shownProducts = computed(() => {
    const selectedCategoryId = this.selectedCategoryId();
    const search = this.productSearch().trim().toLowerCase();
    const products = selectedCategoryId
      ? this.activeProducts().filter((product) => product.categoryId === selectedCategoryId)
      : this.activeProducts();

    if (!search) return products;

    return products.filter((product) => {
      const category = this.categories().find((item) => item.id === product.categoryId);
      const text = [
        product.name,
        product.brand,
        product.description,
        product.code,
        category?.name,
      ]
        .join(' ')
        .toLowerCase();

      return text.includes(search);
    });
  });

  readonly selectedCategoryName = computed(() => {
    const selected = this.categories().find(
      (category) => category.id === this.selectedCategoryId()
    );
    return selected ? `Filtrando por ${selected.name}` : 'Todos los productos activos';
  });

  ngOnInit(): void {
    this.route.queryParamMap.subscribe((params) => {
      const categoryId = Number(params.get('categoryId'));
      this.selectedCategoryId.set(Number.isFinite(categoryId) && categoryId > 0 ? categoryId : null);
    });

    if (!isPlatformBrowser(this.platformId)) {
      this.loading.set(false);
      return;
    }

    this.load();
  }

  clearCategory(): void {
    this.router.navigate(['/products']);
  }

  canAddToCart(): boolean {
    return !(this.session.isAuthenticated() && this.session.claims().role === 'ADMIN');
  }

  addToCart(product: Product): void {
    if (!this.canAddToCart() || product.stock <= 0) return;
    this.cart.add(product);
  }

  private load(): void {
    this.loading.set(true);
    this.error.set('');

    forkJoin({
      categories: this.categoryApi.list(),
      products: this.productApi.list(),
    })
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: ({ categories, products }) => {
          this.categories.set(categories ?? []);
          this.products.set(products ?? []);
        },
        error: () => this.error.set('No se pudieron cargar los productos.'),
      });
  }
}
