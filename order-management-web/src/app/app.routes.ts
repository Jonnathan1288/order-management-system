import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () =>
            import('./shared/components/public-layout/public-layout.component').then(
                (m) => m.PublicLayoutComponent
            ),
        children: [
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            {
                path: 'home',
                loadComponent: () =>
                    import('./features/public/home/home.page').then((m) => m.HomePage),
            },
            {
                path: 'sign-in',
                loadComponent: () =>
                    import('./features/auth/sign-in.page').then((m) => m.SignInPage),
            },
        ],
    },
    { path: '**', redirectTo: '' },
];
