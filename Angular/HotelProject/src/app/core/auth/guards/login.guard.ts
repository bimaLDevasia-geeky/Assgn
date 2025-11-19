import { inject } from "@angular/core";
import {  CanActivateFn, Router } from "@angular/router";
import { AuthService } from "../services/auth.service";
import { map, take } from "rxjs";

// This guard prevents logged-in users from accessing login page
export const loginGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    return authService.currentUser$.pipe(
    take(1), 
    map(user => {
      if (user) {
        // User is already logged in, redirect to dashboard
        if (user.role === 'Admin') {
        return router.createUrlTree(['/admin/dashboard']); 
        }
        return router.createUrlTree(['/home']);
      } else {
        // User is not logged in, allow access to login page
        return true; 
      }
    })
  );
}