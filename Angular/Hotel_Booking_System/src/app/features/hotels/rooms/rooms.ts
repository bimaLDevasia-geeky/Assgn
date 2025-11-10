import { Component, inject, effect, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, catchError } from 'rxjs';
import { Room } from '../../../shared/models/room';
import { RoomService } from './room.service';
import { AsyncAction } from 'rxjs/internal/scheduler/AsyncAction';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-rooms',
  imports: [AsyncPipe],
  templateUrl: './rooms.html',
  styleUrl: './rooms.scss',
})
export class Rooms {
  private route = inject(ActivatedRoute);
  hotelid = signal<string | null>(null);

  private roomsSubject = new BehaviorSubject<Room[]>([]);
  public rooms$ = this.roomsSubject.asObservable();

  constructor(private roomService: RoomService) {
    effect(() => {
      // Get hotel ID from parent route
      const id = this.route.parent?.paramMap.subscribe(params => {
        const hotelId = params.get('hotelid');
        this.hotelid.set(hotelId);

        if (hotelId) {
          this.roomService.getRoomDetails(hotelId).pipe(
            catchError(error => {
              console.error('Error fetching rooms:', error);
              return [];
            })
          ).subscribe(rooms => {
            this.roomsSubject.next(rooms);
          });
        }
      });
  });
}






}
  
