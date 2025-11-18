import { CommonModule, NgOptimizedImage } from '@angular/common';
import { ChangeDetectionStrategy, Component,Signal,signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { minDateTodayValidator, checkOutAfterCheckInValidator } from '../../../shared/validators/date-validators';

export interface Location {
  name: string;
  img: string;
}
@Component({
  selector: 'app-home',
  imports: [ReactiveFormsModule,CommonModule,NgOptimizedImage],
  templateUrl: './home.html',
  styleUrl: './home.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Home {
  
  searchForm: FormGroup;

  location: Signal<Location[]> = signal<Location[]>([
    {name:"Paris", img:"paris.png"},
    {name:"New York", img:"newyork.png"},
    {name:"Santorini", img:"santorini.png"},
    {name:"Tokyo", img:"tokyo.png"},
    {name:"Sydney", img:"sydney.png"},
    {name:"Rome", img:"rome.png"},
  ]);

  constructor(private fb: FormBuilder,private router:Router) {
    this.searchForm = this.fb.group({
      destination: ['',[ Validators.minLength(3)]],
      checkIn: ['', [Validators.required, minDateTodayValidator()]],
      checkOut: ['', [Validators.required, checkOutAfterCheckInValidator()]]
    });

    // Re-validate checkOut when checkIn changes
    this.searchForm.get('checkIn')?.valueChanges.subscribe(() => {
      this.searchForm.get('checkOut')?.updateValueAndValidity();
    });
  }

  getValues(){
    if (this.searchForm.invalid) {
      this.searchForm.markAllAsTouched();
      return;
    }
    let values: { [key: string]: string } = {};
    for (const control in this.searchForm.controls) {
      if (this.searchForm.controls[control].value) {
        values[control] = this.searchForm.controls[control].value;
      }
    }
    this.router.navigate(['/hotel'], { queryParams: values });
  }
 

}
