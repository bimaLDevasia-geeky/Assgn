import { ChangeDetectionStrategy, Component, DestroyRef, inject } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { SidebarService } from './sidebar.service';
import { MatSliderModule } from '@angular/material/slider';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { HotelSearchParams } from '../../../../../shared/types/search-filter.types';

@Component({
  selector: 'app-sidebar',
  imports: [MatSliderModule, ReactiveFormsModule],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Sidebar {
  private destroyref = inject(DestroyRef);
  private router = inject(Router);

  sidebarService = inject(SidebarService);
  
  minPrice = 0;
  maxPrice = 5000;
  
 
  isEditing = this.sidebarService.isEditing;
  
  searchForm: FormGroup;
  filterForm: FormGroup;
  
  constructor(private route: ActivatedRoute, private fb: FormBuilder) {
  
    this.searchForm = this.fb.group({
      destination: [''],
      checkIn: [''],
      checkOut: ['']
    });


    this.filterForm = this.fb.group({
      minPrice: [0],
      maxPrice: [5000],
      starRating: this.fb.array([])
    });


    this.route.parent?.queryParams.pipe(takeUntilDestroyed(this.destroyref)).subscribe(params => {
      this.searchForm.patchValue({
        destination: params['destination'] || '',
        checkIn: params['checkIn'] || '',
        checkOut: params['checkOut'] || ''
      });


      if (params['minPrice']) {
        this.filterForm.patchValue({ minPrice: +params['minPrice'] });
      }
      if (params['maxPrice']) {
        this.filterForm.patchValue({ maxPrice: +params['maxPrice'] });
      }
      if (params['starRating']) {
        const ratings = Array.isArray(params['starRating']) 
          ? params['starRating'] 
          : [params['starRating']];
        this.starRatingArray.clear();
        ratings.forEach((rating: string) => {
          this.starRatingArray.push(new FormControl(+rating));
        });
      }
    });
  }

  get starRatingArray(): FormArray {
    return this.filterForm.get('starRating') as FormArray;
  }

  onStarRatingChange(rating: number, event: Event): void {
    const checked = (event.target as HTMLInputElement).checked;
    if (checked) {
      this.starRatingArray.push(new FormControl(rating));
    } else {
      const index = this.starRatingArray.controls.findIndex(
        control => control.value === rating
      );
      if (index !== -1) {
        this.starRatingArray.removeAt(index);
      }
    }
  }

  isStarRatingChecked(rating: number): boolean {
    return this.starRatingArray.controls.some(control => control.value === rating);
  }

  saveSearch(): void {
    if (this.searchForm.valid) {
      this.updateQueryParams();
      this.sidebarService.changeEditing(true);
    }
  }

  applyFilters(): void {
    if (this.filterForm.valid) {
      this.updateQueryParams();
    }
  }

  private updateQueryParams(): void {
    const params: Partial<HotelSearchParams> = {
      ...this.searchForm.value,
      minPrice: this.filterForm.value.minPrice,
      maxPrice: this.filterForm.value.maxPrice,
      starRating: this.starRatingArray.value
    };

    // Remove empty values
    Object.keys(params).forEach(key => {
      const value = params[key as keyof HotelSearchParams];
      if (value === '' || value === null || value === undefined ||
          (Array.isArray(value) && value.length === 0)) {
        delete params[key as keyof HotelSearchParams];
      }
    });

    // Replace all query params (not merge) to ensure removed filters are cleared
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: params,
      queryParamsHandling: '' // Empty string means replace all params
    });
  }
}
