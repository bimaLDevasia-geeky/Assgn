import { Routes } from '@angular/router';
import { loginGuard } from './core/auth/guards/login.guard';
import { authGuard } from './core/auth/guards/auth.guard';

export const routes: Routes = [
    {
        path: 'login',
        loadComponent: () => import('./core/auth/pages/login/login').then(m => m.Login),
        canActivate: [loginGuard]
    },
    {
        path: 'register',
        loadComponent: () => import('./core/auth/pages/register/register').then(m => m.Register),
        canActivate: [loginGuard]
    },
    {
        path:'',
        loadComponent: () => import('./core/layout/home-layout/home-layout').then(m => m.HomeLayout),
        loadChildren: () => import('./core/layout/home-layout/home-layout.routes').then(m => m.routes)
        // REMOVED userGuard - home should be accessible to everyone
    },

    {
        path:'admin',
        loadComponent: () => import('./core/layout/admin-layout/admin-layout').then(m => m.AdminLayout),
        loadChildren: () => import('./core/layout/admin-layout/admin-layout.routes').then(m => m.routes),
        canActivate: [authGuard]
    },
    {
        path:'',
        pathMatch:'full',
        redirectTo:''
    }
];
