import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { Employee } from "../../../shared/models/employee";
import { Observable, catchError, of, map } from "rxjs";


@Injectable({
    providedIn: 'root'
})
export class EmployeesService {

    private apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

   

    getEmployeesByHotelId(hotelId: string): Observable<Employee[]> {
        return this.http.get<Employee[]>(`${this.apiUrl}/employee`).pipe(
            map(employees => employees.filter(emp => emp.hotelId === hotelId)),
            catchError(error => {
                console.error('Error fetching all employees:', error);
                return of([]);
            })
        );
    }
}