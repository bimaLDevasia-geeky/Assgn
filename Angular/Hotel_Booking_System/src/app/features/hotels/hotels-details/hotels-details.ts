import { Component, effect, input, signal } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';

import { Hotel } from '../../../shared/models/hotel';
import { Employee } from '../../../shared/models/employee';

@Component({
  selector: 'app-hotels-details',
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './hotels-details.html',
  styleUrl: './hotels-details.scss',
})
export class HotelsDetails {

  // Input: receives hotel ID from route (via withComponentInputBinding)
  hotelid = input<string>();
  
  // Signal to store selected employee
  selectedEmployee = signal<Employee | null>(null);

  constructor() {
    effect(() => {
      console.log("Hotel ID in Details:", this.hotelid());
    });
  }

  // Method to handle employee selection from child component
  onEmployeeSelected(employee: Employee): void {
    this.selectedEmployee.set(employee);
    console.log('Selected employee in parent:', employee);
    // You could show a modal, navigate, or display details here
  }
}
