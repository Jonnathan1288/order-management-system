import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Component, PLATFORM_ID, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BusinessApiService } from '../../../data/api/business-api.service';
import { Business } from '../../../data/entities';
import { FloatingCartButtonComponent } from "../floating-cart-button/floating-cart-button.component";
import { FooterComponent } from "../footer/footer.component";
import { HeaderComponent } from "../header/header.component";

@Component({
  selector: 'app-public-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HeaderComponent, FooterComponent, FloatingCartButtonComponent],
  templateUrl: './public-layout.component.html',
  styleUrl: './public-layout.component.css',
})
export class PublicLayoutComponent {
  private readonly businessApi = inject(BusinessApiService);
  private readonly platformId = inject(PLATFORM_ID);

  readonly business = signal<Business | null>(null);

  ngOnInit(): void {
    if (!isPlatformBrowser(this.platformId)) return;

    this.businessApi.getFirst().subscribe({
      next: (business) => this.business.set(business),
      error: () => this.business.set(null),
    });
  }
}
