import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUrl } from '../../../environments/developerenvironment';
import { Booking, CreateBookingCommand, UpdateBookingCommand } from '../types/admin.types';
import { BookingResponse } from '../types/booking.types';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private http = inject(HttpClient);
  private apiUrl = apiUrl;

  getAllBookings(): Observable<Booking[]> {
    return this.http.get<Booking[]>(`${this.apiUrl}/booking`);
  }

  getBookingById(id: string): Observable<Booking> {
    return this.http.get<Booking>(`${this.apiUrl}/booking/${id}`);
  }

  createBooking(command: CreateBookingCommand): Observable<BookingResponse> {
    return this.http.post<BookingResponse>(`${this.apiUrl}/booking`, command);
  }

  updateBooking(id: string, command: UpdateBookingCommand): Observable<Booking> {
    return this.http.put<Booking>(`${this.apiUrl}/booking/${id}`, command);
  }

  deleteBooking(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/booking/${id}`);
  }
}
