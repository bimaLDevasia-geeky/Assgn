import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { BookingService } from '../../../../../../shared/services/booking.service';
import { Booking as BookingModel } from '../../../../../../shared/types/admin.types';
import { catchError } from 'rxjs';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-booking',
  imports: [DatePipe],
  templateUrl: './booking.html',
  styleUrl: './booking.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BookingComponent implements OnInit {
  private bookingService = inject(BookingService);

  bookings = signal<BookingModel[]>([]);
  loading = signal<boolean>(true);

  ngOnInit(): void {
    this.loadBookings();
  }

  loadBookings(): void {
    this.loading.set(true);
    this.bookingService.getAllBookings().pipe(
      catchError((error) => {
        console.error('Error fetching bookings:', error);
        this.loading.set(false);
        throw error;
      })
    ).subscribe({
      next: (bookings: BookingModel[]) => {
        this.bookings.set(bookings);
        this.loading.set(false);
      }
    });
  }

  getStatusClass(status: string): string {
    switch(status.toLowerCase()) {
      case 'confirmed': return 'status-confirmed';
      case 'cancelled': return 'status-cancelled';
      case 'completed': return 'status-completed';
      default: return 'status-pending';
    }
  }
}
