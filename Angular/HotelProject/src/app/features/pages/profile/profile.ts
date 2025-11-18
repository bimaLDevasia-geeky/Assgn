import { ChangeDetectionStrategy, Component, DestroyRef, inject, signal } from '@angular/core';
import { AuthService } from '../../../core/auth/services/auth.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ProfileService } from './profile.service';
import { Customer, Booking } from '../../../shared/types/admin.types';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-profile',
  imports: [CommonModule, RouterLink],
  templateUrl: './profile.html',
  styleUrl: './profile.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Profile {
  user = signal<Customer | null>(null);
  bookings = signal<Booking[]>([]);
  isLoading = signal<boolean>(true);
  ref = inject(DestroyRef);

  constructor(private authService: AuthService, private profileService: ProfileService) {
    this.authService.currentUser$.pipe(takeUntilDestroyed(this.ref)).subscribe(user => {
      if (user != null && user.id) {
        console.log('Fetching profile data for user ID:', user.id);
        this.isLoading.set(true);
        
        // Fetch both user profile and bookings in parallel
        forkJoin({
          profile: this.profileService.getUser(user.id),
          bookings: this.profileService.getUserBookings(user.id)
        }).subscribe({
          next: ({ profile, bookings }) => {
            this.user.set(profile);
            this.bookings.set(bookings);
            this.isLoading.set(false);
          },
          error: (error) => {
            console.error('Error fetching profile data:', error);
            this.user.set(null);
            this.bookings.set([]);
            this.isLoading.set(false);
          }
        });
      } else {
        this.isLoading.set(false);
      }
    });
  }

  getStatusClass(status: string): string {
    const statusMap: { [key: string]: string } = {
      'Confirmed': 'status-confirmed',
      'Pending': 'status-pending',
      'Cancelled': 'status-cancelled',
      'Completed': 'status-completed'
    };
    return statusMap[status] || '';
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    });
  }
}
