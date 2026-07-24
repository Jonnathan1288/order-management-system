import { CommonModule } from '@angular/common';
import { Component, input } from '@angular/core';
import { Business } from '../../../data/entities';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css',
})
export class FooterComponent {
  public business = input<Business | null>(null);
}
