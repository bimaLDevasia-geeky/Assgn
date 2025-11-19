import { ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RoomService } from '../../../shared/services/room.service';
import { Room } from '../../../shared/types/room.types';
import { CommonModule, Location } from '@angular/common';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { AuthService } from '../../../core/auth/services/auth.service';

@Component({
  selector: 'app-hotel-details',
  imports: [CommonModule],
  templateUrl: './hotel-details.html',
  styleUrl: './hotel-details.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HotelDetails implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private roomService = inject(RoomService);
  private destroyRef = inject(DestroyRef);

  hotelId = signal<string>('');
  rooms= signal<Room[]>([]);
  private location = inject(Location);
  private authservice = inject(AuthService);
  checkInDate = signal<string>('');
  checkOutDate = signal<string>('');


  ngOnInit(): void {
    this.route.queryParams.pipe(takeUntilDestroyed(this.destroyRef)).subscribe(params => {
      const id = params['id'];
      const checkIn = params['checkIn'];
      const checkOut = params['checkOut'];
      if (id) {
        this.hotelId.set(id);
        this.checkInDate.set(checkIn );
        this.checkOutDate.set(checkOut);
        console.log('Fetching rooms with params:', { id, checkIn, checkOut }); 
        this.roomService.getRooms(id, checkIn, checkOut).subscribe(rooms => {
          this.rooms.set(rooms);
        });
      } else {
        // Navigate back if no ID
        console.log('No hotel ID found, navigating back to hotels');
        this.router.navigate(['/hotel']);
      }
    });
  }

  bookRoom(roomId: string): void {

    const currentUser = this.authservice.currentUser$.pipe(takeUntilDestroyed(this.destroyRef)).subscribe(user => {
      if (!user || user.role !== 'Customer') {
        
    // Navigate to login or booking page
    this.router.navigate(['/login'], {
      queryParams: { 
        roomId,
        hotelId: this.hotelId(),
        checkIn: this.checkInDate(),
        checkOut: this.checkOutDate()
      }
    });
      } else {
        // Navigate to booking page
        this.router.navigate(['/booking'], {
          queryParams: { 
            roomId,
            hotelId: this.hotelId(),
            checkIn: this.checkInDate(),
            checkOut: this.checkOutDate()
          }
        });
      }


    });
  }


  goBack(): void {
    this.location.back();
  }
}
