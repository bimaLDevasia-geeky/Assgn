import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { Room } from "../../../shared/models/room";


@Injectable({
    providedIn: 'root'
})
export class RoomService {

    constructor(private http: HttpClient) {

    }
    private apiUrl = environment.apiUrl;
    
    getRoomDetails(hotelId: string) {
        return this.http.get<Room[]>(`${this.apiUrl}/room/byhotel?hotelId=${hotelId}`);
    }
}