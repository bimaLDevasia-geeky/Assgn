import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { catchError, filter, switchMap, take, throwError } from 'rxjs';
import { ToastService } from '../../../shared/services/toast.sercie';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getAccessToken();
  const toastService = inject(ToastService);
  
  console.log('🔐 Auth Interceptor:', {
    url: req.url,
    method: req.method,
    hasToken: !!token,
    token: token ? `${token.substring(0, 20)}...` : 'none'
  });
  
  // Helper function to add the header
  const addTokenHeader = (request: any, token: string) => {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  };

  let authReq = req;

  if (req.url.includes('refresh')) {
    return next(req);
  }

  if (token) {
    authReq = addTokenHeader(req, token);
    console.log('✅ Token added to request');
  } else {
    console.log('⚠️ No token available');
  }

 
  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      
    
      if (error.status === 401) {
        console.log('🔄 401 Unauthorized - Attempting token refresh');

        if (req.url.includes('/auth/refresh') || req.url.includes('/auth/login')) {
             authService.logout();
             return throwError(() => error);
        }


        // Case A: We are NOT already refreshing
        if (!authService.isRefreshing) {
       
          authService.isRefreshing = true;
          authService.updateTokenStream(null)

          return authService.refreshToken().pipe(
            switchMap((tokenResponse: any) => {
              console.log('✅ Token refreshed successfully');
              return next(addTokenHeader(req, tokenResponse.token));
            }),
            catchError((refreshErr) => {
              console.error('❌ Token refresh failed:', refreshErr);
              authService.isRefreshing = false;
              authService.logout();
                toastService.error('Session expired. Please log in again.');
              return throwError(() => refreshErr);
            })
          );
        } 
     
        else {
          console.log('⏳ Waiting for ongoing token refresh...');
          return authService.refreshToken$.pipe(
            filter(token => token !== null),
            take(1),
            switchMap((token) => {
              console.log('✅ Using refreshed token');
              return next(addTokenHeader(req, token));
            })
          );
        }
      }
      
      if (error.status === 403) {
        console.error('🚫 403 Forbidden - Authorization denied', {
          url: error.url,
          hasToken: !!token
        });
      }

   
      return throwError(() => error);
    })
  );
};