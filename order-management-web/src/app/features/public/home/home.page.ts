import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Component, PLATFORM_ID, inject, signal } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { finalize } from 'rxjs';
import { BusinessApiService } from '../../../data/api/business-api.service';
import { Business } from '../../../data/entities';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.page.html',
  styleUrl: './home.page.css',
})
export class HomePage {
  private readonly businessApi = inject(BusinessApiService);
  private readonly sanitizer = inject(DomSanitizer);
  private readonly platformId = inject(PLATFORM_ID);

  readonly loading = signal(true);
  readonly error = signal('');
  readonly business = signal<Business | null>(null);

  ngOnInit(): void {
    if (!isPlatformBrowser(this.platformId)) {
      this.loading.set(false);
      return;
    }

    this.loadBusiness();
  }

  templateHtml(): SafeHtml | null {
    const business = this.business();
    if (!business?.useTemplate || !business.template) return null;
    return this.sanitizer.bypassSecurityTrustHtml(business.template);
  }

  private loadBusiness(): void {
    this.loading.set(true);
    this.error.set('');

    this.businessApi
      .getFirst()
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (business) => this.business.set(business ?? null),
        error: () => this.error.set('No se pudo cargar la informacion publica del negocio.'),
      });
  }
}
