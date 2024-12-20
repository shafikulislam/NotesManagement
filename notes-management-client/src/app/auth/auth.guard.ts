import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../../../src/app/core/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const isAuthenticated = this.authService.isAuthenticated();
    const isAuthRoute = state.url.startsWith('/auth');

    if (isAuthenticated && isAuthRoute) {
      this.router.navigate(['/dashboard']); // Redirect to dashboard if already logged in
      return false;
    }

    if (!isAuthenticated && !isAuthRoute) {
      this.router.navigate(['/auth/login']);
      return false;
    }

    return true;
  }

}
