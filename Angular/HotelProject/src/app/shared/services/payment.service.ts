import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUrl } from '../../../environments/developerenvironment';
import { PaymentVerification } from '../types/booking.types';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private http = inject(HttpClient);
  private apiUrl = apiUrl;

  verifyPayment(verification: PaymentVerification): Observable<any> {
    return this.http.post(`${this.apiUrl}/payment/confirm`, verification);
  }
}
