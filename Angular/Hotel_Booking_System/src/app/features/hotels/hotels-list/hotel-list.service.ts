import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { BehaviorSubject, tap, catchError, of } from "rxjs";
import { Hotel } from "../../../shared/models/hotel";
import { HttpClient } from "@angular/common/http";



@Injectable({ 
    providedIn: 'root'
})
export class HotelListService {

private apiUrl = environment.apiUrl;

private hotelsSubject = new BehaviorSubject<Hotel[]>([]);
public hotels$ = this.hotelsSubject.asObservable();

    constructor(private  http:HttpClient) {}

    loadHotels():void {
        this.http.get<Hotel[]>(`${this.apiUrl}/hotel`).pipe(
            tap(data=>{
                this.hotelsSubject.next(data);
            }),
            catchError(error => {
                console.error('Error loading hotels:', error);
                return of([]);
            })
        ).subscribe();
        }
}   