import { FormControl } from '@angular/forms';

export interface SearchFormValue {
  destination: string;
  checkIn: string;
  checkOut: string;
}

export interface FilterFormValue {
  minPrice: number;
  maxPrice: number;
  starRating: number[];
}

export interface SearchFormControls {
  destination: FormControl<string>;
  checkIn: FormControl<string>;
  checkOut: FormControl<string>;
}

export interface FilterFormControls {
  minPrice: FormControl<number>;
  maxPrice: FormControl<number>;
  starRating: FormControl<number[]>;
}

export interface HotelSearchParams extends SearchFormValue, FilterFormValue {
  sortBy?: string;
}
