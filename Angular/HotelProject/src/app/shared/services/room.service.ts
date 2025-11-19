import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUrl } from '../../../environments/developerenvironment';
import { Room } from '../types/room.types';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private http = inject(HttpClient);
  private apiUrl = apiUrl;

  getRooms(hotelId: string, checkIn: string, checkOut: string) {
    return this.http.get<Room[]>(`${this.apiUrl}/room/filter`, { 
      params: { hotelId, checkIn: checkIn , checkOut: checkOut  }
    });
  }
  getRoom(roomId: string): Observable<Room> {
    return this.http.get<Room>(`${this.apiUrl}/room/${roomId}`);
  }
}