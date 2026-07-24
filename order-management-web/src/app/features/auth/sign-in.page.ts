import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { environment } from '../../../environments/environment';
import { AuthSessionService } from '../../core/auth-session.service';
import { AuthApiService } from '../../data/api/auth-api.service';

@Component({
  selector: 'app-sign-in.page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './sign-in.page.html',
  styleUrl: './sign-in.page.css',
})
export class SignInPage {
  private readonly authApi = inject(AuthApiService);
  private readonly session = inject(AuthSessionService);
  private readonly router = inject(Router);
  private readonly formBuilder = inject(FormBuilder);

  readonly apiBase = environment.API_URL;
  readonly loading = signal(false);
  readonly error = signal('');

  readonly form = this.formBuilder.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]],
  });

  ngOnInit(): void {
    if (this.session.isAuthenticated()) {
      this.router.navigateByUrl('/home');
    }
  }

  submit(): void {
    if (this.form.invalid) return;

    this.error.set('');
    this.loading.set(true);

    this.authApi
      .login({
        email: this.form.controls.email.value,
        password: this.form.controls.password.value,
      })
      .subscribe({
        next: (response) => {
          const token = response.token;
          if (!token) {
            this.loading.set(false);
            return;
          }

          this.session.setToken(token);
          this.router.navigateByUrl('/home');
        },
        error: () => {
          this.error.set('Credenciales invalidas o servicio no disponible.');
          this.loading.set(false);
        },
        complete: () => this.loading.set(false),
      });
  }
}
