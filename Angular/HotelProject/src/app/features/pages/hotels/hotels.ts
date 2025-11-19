import { Component, ChangeDetectionStrategy, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Hotellist } from './components/hotellist/hotellist';
import { Sidebar } from './components/sidebar/sidebar';
import { HotelService } from './hotel.service';
import { HotelSearchParams } from '../../../shared/types/search-filter.types';
import { Hotel } from '../../../shared/types/hotel.types';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../../shared/services/toast.sercie';

@Component({
  selector: 'app-hotels',
  imports: [Hotellist, Sidebar, CommonModule],
  templateUrl: './hotels.html',
  styleUrl: './hotels.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Hotels implements OnInit {
  private destroyRef = inject(DestroyRef);
  private hotelService = inject(HotelService);
  private router = inject(Router);
  
  hotels = signal<Hotel[]>([]);
  loading = signal<boolean>(false);
  
  constructor(private route: ActivatedRoute, private toast : ToastService) {

    this.hotelService.hotels$.pipe(takeUntilDestroyed(this.destroyRef)).subscribe(hotels => {
      this.hotels.set(hotels);
    });
    

    this.hotelService.loading$.pipe(takeUntilDestroyed(this.destroyRef)).subscribe(loading => {
      this.loading.set(loading);
    });
    

    this.route.queryParams.pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe(params => {
      if (Object.keys(params).length > 0) {
        this.fetchHotels(params);
      }
    });
  }

  ngOnInit(): void {

    const initialParams = this.route.snapshot.queryParams;
    if (Object.keys(initialParams).length > 0) {
      // Params exist, fetch immediately
      this.fetchHotels(initialParams);
    }

  }

  onSortByChanged(sortBy: string): void {
    const currentParams = { ...this.route.snapshot.queryParams };
    const params: Partial<HotelSearchParams> = {
      ...currentParams,
      sortBy
    };
    

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { sortBy },
      queryParamsHandling: 'merge'
    });
  }

  private fetchHotels(params: Partial<HotelSearchParams>): void {
    this.hotelService.getHotels(params)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (response) => {
          console.log('Hotels fetched:', response);
        },
        error: (error) => {
          console.error('Error fetching hotels:', error);
          this.toast.error('Failed to load hotels. Please try again later.');
        }
      });
  }
}
