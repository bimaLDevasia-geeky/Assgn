import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-payment-failure',
  imports: [CommonModule],
  templateUrl: './payment-failure.html',
  styleUrl: './payment-failure.scss',
  standalone: true
})
export class PaymentFailure implements OnInit {
  errorMessage = signal<string>('');

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.errorMessage.set(params['error'] || 'Payment processing failed');
    });
  }

  tryAgain(): void {
    this.router.navigate(['/booking'], {
      queryParams: this.route.snapshot.queryParams
    });
  }

  goToHome(): void {
    this.router.navigate(['/home']);
  }
}
