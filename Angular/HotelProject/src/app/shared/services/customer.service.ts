import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUrl } from '../../../environments/developerenvironment';
import { Customer, CreateCustomerCommand, UpdateCustomerCommand } from '../types/admin.types';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private http = inject(HttpClient);
  private apiUrl = apiUrl;

  getAllCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(`${this.apiUrl}/customer`);
  }

  getCustomerById(id: string): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiUrl}/customer/${id}`);
  }

  createCustomer(command: CreateCustomerCommand): Observable<Customer> {
    return this.http.post<Customer>(`${this.apiUrl}/customer`, command);
  }

  updateCustomer(id: string, command: UpdateCustomerCommand): Observable<Customer> {
    return this.http.put<Customer>(`${this.apiUrl}/customer/${id}`, command);
  }

  deleteCustomer(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/customer/${id}`);
  }
}
