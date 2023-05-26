import { Route } from '@angular/router';
import { AuthGuard } from '@auth0/auth0-angular';

export const ROUTES: Route[] = [
  {
    path: '',
    pathMatch: 'full',
    loadComponent: () => import('./features/home/home.component').then(mod => mod.HomeComponent)
  },
  {
    path: 'profile',
    loadComponent: () => import('./features/profile/profile.component').then(mod => mod.ProfileComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'callback',
    loadComponent: () => import('./features/callback/callback.component').then(mod => mod.CallbackComponent)
  },
  {
    path: '**',
    loadComponent: () => import('./features/not-found/not-found.component').then(mod => mod.NotFoundComponent)
  },
];