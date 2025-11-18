import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUrl } from '../../../environments/developerenvironment';
import { RoomType, CreateRoomTypeCommand, UpdateRoomTypeCommand } from '../types/admin.types';

@Injectable({
  providedIn: 'root'
})
export class RoomTypeService {
  private http = inject(HttpClient);
  private apiUrl = apiUrl;

  getAllRoomTypes(): Observable<RoomType[]> {
    return this.http.get<RoomType[]>(`${this.apiUrl}/roomtype`);
  }

  getRoomTypeById(id: string): Observable<RoomType> {
    return this.http.get<RoomType>(`${this.apiUrl}/roomtype/${id}`);
  }

  createRoomType(command: CreateRoomTypeCommand): Observable<RoomType> {
    return this.http.post<RoomType>(`${this.apiUrl}/roomtype`, command);
  }

  updateRoomType(id: string, command: UpdateRoomTypeCommand): Observable<RoomType> {
    return this.http.put<RoomType>(`${this.apiUrl}/roomtype/${id}`, command);
  }

  deleteRoomType(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/roomtype/${id}`);
  }
}
