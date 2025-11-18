import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { apiUrl } from "../../../../environments/developerenvironment";
import { Observable } from "rxjs";
import { Customer, Booking } from "../../../shared/types/admin.types";

@Injectable({
    providedIn: 'root'
})
export class ProfileService {
    
    constructor(private http: HttpClient) {}
    private apiUrl = apiUrl; 
    
    getUser(id: string | null): Observable<Customer> {
        return this.http.get<Customer>(`${this.apiUrl}/customer/${id}`);
    }

    getUserBookings(userId: string): Observable<Booking[]> {
        return this.http.get<Booking[]>(`${this.apiUrl}/booking/customer/${userId}`);
    }
}