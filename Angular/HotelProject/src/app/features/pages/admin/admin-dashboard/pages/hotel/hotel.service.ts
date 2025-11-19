import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { apiUrl } from "../../../../../../../environments/developerenvironment";
import { Hotel, HotelFormType } from "../../../../../../shared/types/admin.hotel.types";

@Injectable({  
    providedIn: 'root'
})

export class HotelService {
    constructor(private http: HttpClient) {}
    private apiUrl = apiUrl;

    loadHotels() {
        return this.http.get<Hotel[]>(`${this.apiUrl}/hotel`);
    }

    edithotel(hotelId: string, hotel: HotelFormType) {
        return this.http.put<Hotel>(`${this.apiUrl}/hotel/${hotelId}`, hotel);
    }
    deletehotel(hotelId: string) {
        return this.http.delete<void>(`${this.apiUrl}/hotel/${hotelId}`);
    }
    addhotel(hotel: HotelFormType) {
        return this.http.post<Hotel>(`${this.apiUrl}/hotel`, hotel);
    }
}