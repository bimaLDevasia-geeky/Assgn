import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-payment-success',
  imports: [CommonModule, RouterLink],
  templateUrl: './payment-success.html',
  styleUrls: ['./payment-success.scss'],
  standalone: true
})
export class PaymentSuccess implements OnInit {
  bookingId = signal<string>('');
  paymentId = signal<string>('');

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.bookingId.set(params['bookingId'] || '');
      this.paymentId.set(params['paymentId'] || '');
    });
  }

  goToHome(): void {
    this.router.navigate(['/home']);
  }
}
