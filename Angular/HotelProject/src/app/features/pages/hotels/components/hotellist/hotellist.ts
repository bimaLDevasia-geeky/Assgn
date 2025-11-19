import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, DestroyRef, effect, inject, input, OnInit, output } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Hotel } from '../../../../../shared/types/hotel.types';
import { faStar as faSolidStar } from '@fortawesome/free-solid-svg-icons';
import { faStar as faRegularStar } from '@fortawesome/free-regular-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'app-hotellist',
  imports: [CommonModule, FormsModule,FontAwesomeModule],
  templateUrl: './hotellist.html',
  styleUrl: './hotellist.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Hotellist implements OnInit {

  private ref = inject(DestroyRef);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  
  // Input from parent
  hotels = input<Hotel[]>([]);
  loading = input<boolean>(false);

  filledStarIcon = faSolidStar;
  emptyStarIcon = faRegularStar;
  totalStars = [1, 2, 3, 4, 5];
  
  // Output to parent
  sortByChanged = output<string>();
  
  list: string[] = [ 'Price Low to High','Price High to Low', 'Guest Rating'];
  sortOption: string = this.list[0];
  destination: string = '';

  constructor() {
    this.route.parent?.queryParams.pipe(takeUntilDestroyed(this.ref)).subscribe(params => {
      this.destination = params['destination'] || 'All';
      // Set sortOption from params if exists
      if (params['sortBy']) {
        this.sortOption = params['sortBy'];
      }
    });

    // Debug: Log hotels when they change
    
  }

  ngOnInit(): void {
    // Emit initial sort option if not in query params
    const currentParams = this.route.snapshot.parent?.queryParams;
    if (!currentParams?.['sortBy']) {
      // Emit the default sort option on initialization
      this.sortByChanged.emit(this.sortOption);
    }
  }

  onSortChange(): void {
    this.sortByChanged.emit(this.sortOption);
  }

  getRatingLabel(rating: number | null): string {
    if (!rating) return 'Not Rated';
    if (rating >= 9) return 'Excellent';
    if (rating >= 8) return 'Very Good';
    if (rating >= 7) return 'Good';
    if (rating >= 6) return 'Pleasant';
    return 'Average';
  }

  viewHotelDetails(id: string): void {
    this.router.navigate(['/hotel-details'], { 
      queryParams: { id },
      queryParamsHandling: 'merge'
    });
  }
}
