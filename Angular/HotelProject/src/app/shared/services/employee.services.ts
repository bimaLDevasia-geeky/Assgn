import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { apiUrl } from "../../../environments/developerenvironment";
import { Employee } from "../types/admin.types";
import { Observable } from "rxjs";

export interface CreateEmployeeCommand {
    hotelId: string;
    fullName: string;
    email: string;
    position: string;
}

export interface UpdateEmployeeCommand {
    id: string;
    fullName: string;
    email: string;
    position: string;
}

@Injectable({  
    providedIn: 'root'
})
export class EmployeeService {
    constructor(private http: HttpClient) {}

    private apiUrl = apiUrl;

    getEmployeesByHotel(hotelId: string): Observable<Employee[]> {
        return this.http.get<Employee[]>(`${this.apiUrl}/employee/byhotel`, { params: { hotelId } });
    }
    getEmployeeById(id: string): Observable<Employee> {
        return this.http.get<Employee>(`${this.apiUrl}/employee/${id}`);
    }

    createEmployee(command: CreateEmployeeCommand): Observable<Employee> {
        return this.http.post<Employee>(`${this.apiUrl}/employee`, command);
    }

    updateEmployee(command: UpdateEmployeeCommand): Observable<Employee> {
        return this.http.put<Employee>(`${this.apiUrl}/employee/${command.id}`, command);
    }

    deleteEmployee(id: string): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/employee/${id}`);
    }
}