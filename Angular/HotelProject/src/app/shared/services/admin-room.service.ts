import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUrl } from '../../../environments/developerenvironment';
import { Room, CreateRoomCommand, UpdateRoomCommand } from '../types/admin.types';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private http = inject(HttpClient);
  private apiUrl = apiUrl;

  getAllRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(`${this.apiUrl}/room`);
  }

  getRoomsByHotelId(hotelId: string): Observable<any[]> {
    return this.http.get<Room[]>(`${this.apiUrl}/room/byhotel?hotelId=${hotelId}`);
  }

  getRoomById(id: string): Observable<Room> {
    return this.http.get<Room>(`${this.apiUrl}/room/${id}`);
  }

  createRoom(command: CreateRoomCommand): Observable<Room> {
    return this.http.post<Room>(`${this.apiUrl}/room`, command);
  }

  updateRoom(id: string, command: UpdateRoomCommand): Observable<Room> {
    return this.http.put<Room>(`${this.apiUrl}/room/${id}`, command);
  }

  deleteRoom(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/room/${id}`);
  }
}
