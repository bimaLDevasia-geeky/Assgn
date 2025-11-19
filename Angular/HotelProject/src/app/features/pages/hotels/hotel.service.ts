import { HttpClient, HttpParams } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable, tap } from "rxjs";
import { Hotel } from "../../../shared/types/hotel.types";
import { HotelSearchParams } from "../../../shared/types/search-filter.types";
import { apiUrl } from "../../../../environments/developerenvironment";

@Injectable({
    providedIn:'root'
})
export class HotelService {
    private http = inject(HttpClient);
    
    private hotelsSubject = new BehaviorSubject<Hotel[]>([]);
    hotels$ = this.hotelsSubject.asObservable();
    
    private loadingSubject = new BehaviorSubject<boolean>(false);
    loading$ = this.loadingSubject.asObservable();
    
    
    private apiUrl = apiUrl;
    
    getHotels(params: Partial<HotelSearchParams>): Observable<Hotel[]> {
        this.loadingSubject.next(true);
        
       
        const cleanParams: Record<string, string | string[]> = {};
        
        Object.keys(params).forEach(key => {
            const value = params[key as keyof HotelSearchParams];
            if (value !== null && value !== undefined && value !== '') {
                if (Array.isArray(value) && value.length > 0) {
                    cleanParams[key] = value.map(String);
                } else if (!Array.isArray(value)) {
                    cleanParams[key] = String(value);
                }
            }
        });
        
        const httpParams = new HttpParams({ fromObject: cleanParams });
        
        return this.http.get<Hotel[]>(`${this.apiUrl}/hotel/filter`, { params: httpParams }).pipe(
            tap(response => {
                this.hotelsSubject.next(response);
                this.loadingSubject.next(false);
            })
        );
    }
    
    // setHotels(hotels: Hotel[]): void {
    //     this.hotelsSubject.next(hotels);
    // }
    
    // clearHotels(): void {
    //     this.hotelsSubject.next([]);
    // }
}