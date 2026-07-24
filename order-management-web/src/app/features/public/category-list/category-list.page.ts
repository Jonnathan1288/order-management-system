import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Component, computed, CUSTOM_ELEMENTS_SCHEMA, inject, PLATFORM_ID, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CategoryApiService } from '../../../data/api/category-api.service';
import { Category } from '../../../data/entities';

@Component({
  selector: 'app-category-list-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './category-list.page.html',
  styleUrl: './category-list.page.css',
})
export class CategoryListPage {
  private readonly router = inject(Router);
  private readonly categoryApi = inject(CategoryApiService);
  private readonly platformId = inject(PLATFORM_ID);

  readonly loading = signal(true);
  readonly error = signal('');
  readonly categories = signal<Category[]>([]);
  readonly categorySearch = signal('');

  readonly visibleCategories = computed(() => {
    const search = this.categorySearch().trim().toLowerCase();
    if (!search) return this.categories();

    return this.categories().filter((category) => {
      const text = [category.name, category.description].join(' ').toLowerCase();
      return text.includes(search);
    });
  });

  ngOnInit(): void {
    if (!isPlatformBrowser(this.platformId)) {
      this.loading.set(false);
      return;
    }

    this.load();
  }

  filterByCategory(category: Category): void {
    this.router.navigate(['/products'], {
      queryParams: { categoryId: category.id },
    });
  }

  trackCategory(_: number, category: Category): number {
    return category.id;
  }

  private load(): void {
    this.loading.set(true);
    this.error.set('');

    this.categoryApi
      .list()
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (categories) => this.categories.set(categories ?? []),
      });
  }
}
