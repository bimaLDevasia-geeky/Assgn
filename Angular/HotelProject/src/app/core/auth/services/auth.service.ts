import { Injectable, signal } from "@angular/core";
import { apiUrl } from "../../../../environments/developerenvironment";
import { BehaviorSubject, catchError, Observable, tap, throwError } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { AuthResponse, LoginRequest, User } from "../../../shared/types/login.types";
import { ToastService } from "../../../shared/services/toast.sercie";



const ClaimTypes = {
  id: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier',
  email: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress',
  role: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
};


@Injectable({
    providedIn:'root'
})
export class AuthService { 

    apiUrl = apiUrl;
    private currentUserSubject = new BehaviorSubject<User | null>(null);
    public currentUser$ = this.currentUserSubject.asObservable();

    private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
    public isRefreshing = false;
    public refreshToken$ = this.refreshTokenSubject.asObservable();
    
    constructor(private http: HttpClient, private toast: ToastService) {
        this.loadInitialUser();
    }

    login(formData: { email: string | null; password: string | null; }): Observable<AuthResponse> {
 
    return this.http.post<AuthResponse>(`${this.apiUrl}/auth/login`, formData,{ withCredentials: true }).pipe(
        tap((response: AuthResponse) => {
          
            const user: User = {
              id: response.id,
              email: response.email,
              role: response.role
            };
            this.currentUserSubject.next(user);
            localStorage.setItem('token', response.token);
        })
    );
    }
    public updateTokenStream(token: string | null) {
    this.refreshTokenSubject.next(token);
    }

    getAccessToken(): string | null {
    return localStorage.getItem('token');
    }

    refreshToken(): Observable<any> {
    
    return this.http.post<{token: string}>(`${this.apiUrl}/auth/refresh`,{},{ withCredentials: true }).pipe(
      tap((response) => {
        this.isRefreshing = false;
        localStorage.setItem('token', response.token);
        this.refreshTokenSubject.next(response.token);
        }),
        catchError((error) => {
          console.error('Refresh token error:', error.message,error);
            this.isRefreshing = false;
            return throwError(() => error);
        })
      );
    }

    logout(): void {

        this.currentUserSubject.next(null);
        localStorage.removeItem('token');
        this.http.post(`${this.apiUrl}/auth/logout`, {},{ withCredentials: true }).subscribe({
            next: () => {
                console.log('Logged out successfully on server.'); 
                this.toast.success('Logged out successfully.');     
            }
        });
    }

    private loadInitialUser() {
    // 1. Get the token from storage
    const token = localStorage.getItem('token');
    if (!token) {
      return; // No user logged in
    }
    try {
      const user = this.decodeToken(token);
      if (user) {
        // 3. Push the user into our state
        this.currentUserSubject.next(user);
        // Remove toast on initial load - too spammy
        console.log('User loaded from token:', user);
      }
    } catch (error) {
      console.error('Invalid token, removing...', error);
      this.logout(); // The token is bad, so log them out
    }
  }




    private decodeToken(token: string): User | null {
  try {
    
    const payloadBase64 = token.split('.')[1];
    
    
    const decodedJson = atob(payloadBase64);

    const payload = JSON.parse(decodedJson);

   
   const user: User = {
 id: payload[ClaimTypes.id] || null,
 email: payload[ClaimTypes.email] || null,
 role: payload[ClaimTypes.role] || null
};

    if(!user.id || !user.email || !user.role) {
        return null;
    }
    return user;

  } catch (error) {
    console.error('Failed to decode token:', error);
    return null;
  }
}

 }