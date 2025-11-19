import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthService } from "../services/auth.service";
import { map, take } from "rxjs";
import { ToastService } from "../../../shared/services/toast.sercie";

// This guard protects routes that require authentication
export const userGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);
    const toastService = inject(ToastService);
    return authService.currentUser$.pipe(
        take(1), 
        map(user => {
            if (user) {
                // User is logged in, allow access
                if (user.role === 'Customer') {
                    return true;
                }
                    
                return router.createUrlTree(['/home']);
            } else {
                // User is not logged in, redirect to login with returnUrl
                toastService.error('You must be logged in to access this page.');
                return router.createUrlTree(['/login'], {
                    queryParams: { returnUrl: state.url }
                });
            }
        })
    );
}
