import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = async (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  // Wait for session to load from storage
  await authService.waitForSessionLoad();

  if (authService.isAuthenticated()) {
    return true;
  }
 
  router.navigate(['/login']);
  return false;
 
  //  return true;
};
