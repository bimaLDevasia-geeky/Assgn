import { Component, input, OnInit } from '@angular/core';
import { ReviewService } from './review.service';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { AsyncPipe } from '@angular/common';


@Component({
  selector: 'app-reviews',
  imports: [AsyncPipe],
  templateUrl: './reviews.html',
  styleUrl: './reviews.scss',
})
export class Reviews implements OnInit {
  // Must match the route parameter name exactly: 'hotelid'
  hotelid : string | null = null;
  private reviews=new BehaviorSubject<any[]>([]);
  public reviews$ = this.reviews.asObservable();

  constructor(private reviewService: ReviewService,private route: ActivatedRoute) {
    this.route.parent?.paramMap.subscribe((params) => {
      this.hotelid = params.get('hotelid');
      console.log("Hotel ID in Reviews:", this.hotelid);
    });
  }

  ngOnInit(): void {
    // You can use the hotelid here
    if (this.hotelid) {
      this.reviewService.getReview(this.hotelid).subscribe(reviews => {
        
        this.reviews.next(reviews);
      });
    }
  }

  // Generate array for filled stars
  getFilledStars(rating: number): number[] {
    return Array(Math.floor(rating)).fill(0);
  }

  // Generate array for empty stars
  getEmptyStars(rating: number): number[] {
    if(this.hasHalfStar(rating)) {
      return Array(4 - Math.floor(rating)).fill(0);
    }
    return Array(5 - Math.floor(rating)).fill(0);
  }

  // Check if there's a half star
  hasHalfStar(rating: number): boolean {

   if(rating % 1 !== 0) {
     return true;
   }
   return false;
  }
}
