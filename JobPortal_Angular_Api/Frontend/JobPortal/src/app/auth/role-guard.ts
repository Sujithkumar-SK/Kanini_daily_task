import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { Auth } from '../service/auth';

export const roleGuard: CanActivateFn = (route) => {
  const auth = inject(Auth);
  const router = inject(Router);
  
  const expectedRoles: string[] = route.data['roles'];
  const user = auth.getCurrentUser();
  
  if (!user || !user.role || !expectedRoles || expectedRoles.length === 0) {
    router.navigate(['/login']);
    return false;
  }
  
  if (expectedRoles.includes(user.role)) {
    return true;
  }
  
  // Not allowed - redirect to login
  router.navigate(['/login']);
  return false;
};
