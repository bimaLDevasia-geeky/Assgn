import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";

@Injectable({
    providedIn: 'root'
})
export class ReviewService {
    constructor(private http: HttpClient) {}
    private apiUrl = environment.apiUrl;

    getReview(hotelId: string) {
        return this.http.get<any[]>(`${this.apiUrl}/review/byhotel?hotelId=${hotelId}`);
    }   
}
