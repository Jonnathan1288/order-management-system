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
                path: 'products',
                loadComponent: () =>
                    import('./features/public/product-list/product-list.page').then((m) => m.ProductListPage),
            },
            {
                path: 'categories',
                loadComponent: () =>
                    import('./features/public/category-list/category-list.page').then((m) => m.CategoryListPage),
            },
            {
                path: 'sign-in',
                loadComponent: () =>
                    import('./features/auth/sign-in.page').then((m) => m.SignInPage),
            },
            {
                path: 'cart',
                loadComponent: () =>
                    import('./features/public/shopping-cart/shopping-cart.page').then(
                        (m) => m.ShoppingCartPage
                    ),
            },
        ],
    },
    { path: '**', redirectTo: '' },
];

