import { Component, OnInit, output } from '@angular/core';
import { EmployeesService } from './employees.service';
import { Observable } from 'rxjs';
import { Employee } from '../../../shared/models/employee';
import { AsyncPipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-employees',
  imports: [AsyncPipe],
  templateUrl: './employees.html',
  styleUrl: './employees.scss',
})
export class Employees implements OnInit {
  hotelid: string | null = null;
  employees$ = new Observable<Employee[]>();

  // Output: emit when an employee is selected
  employeeSelected = output<Employee>();
  
  constructor(
    private employeeService: EmployeesService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // Get hotel ID from parent route
    this.hotelid = this.route.parent?.snapshot.paramMap.get('hotelid') ?? null;
    console.log("Hotel ID in Employees:", this.hotelid);
    
    // Fetch employees when component initializes
    if (this.hotelid) {
      this.employees$ = this.employeeService.getEmployeesByHotelId(this.hotelid);
    }
  }

  // Method to handle employee selection
  onEmployeeClick(employee: Employee): void {
    console.log('Employee clicked:', employee);
    this.employeeSelected.emit(employee);
  }
}

