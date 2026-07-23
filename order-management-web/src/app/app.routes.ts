import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        children: [
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            {
                path: 'home',
                data: { section: 'home' },
                loadComponent: () =>
                    import('./features/public/home/home.page').then((m) => m.HomePage),
            },
        ]
    }
];
