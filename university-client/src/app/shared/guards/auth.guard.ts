import { inject } from '@angular/core';
import { CanActivateFn, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const url: string = state.url;
  const isLoggedIn = authService.isLoggedIn();

  if (url !== '/login' && url !== '/register') {
    console.log(url)
    if (!isLoggedIn) {
      authService.logout();
    }
    return true;
  }

  return router.parseUrl('/');
};
