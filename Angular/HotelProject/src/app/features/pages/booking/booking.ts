import { ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RoomService } from '../../../shared/services/room.service';
import { BookingService } from '../../../shared/services/booking.service';
import { AuthService } from '../../../core/auth/services/auth.service';
import { PaymentService } from '../../../shared/services/payment.service';
import { Room } from '../../../shared/types/room.types';
import { CreateBookingCommand } from '../../../shared/types/admin.types';
import { PaymentVerification, BookingResponse } from '../../../shared/types/booking.types';
import { ToastService } from '../../../shared/services/toast.sercie';
import { minDateTodayValidator, checkOutAfterCheckInValidator } from '../../../shared/validators/date-validators';
import { catchError, of, switchMap } from 'rxjs';
import { razorpayKey } from '../../../../environments/developerenvironment';
import { EmailService } from '../../../shared/services/email.service';

declare var Razorpay: any;

@Component({
  selector: 'app-booking',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './booking.html',
  styleUrl: './booking.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BookingPage implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private destroyRef = inject(DestroyRef);
  private roomService = inject(RoomService);
  private bookingService = inject(BookingService);
  private authService = inject(AuthService);
  private paymentService = inject(PaymentService);
  private location = inject(Location);
  private toast = inject(ToastService);

  room = signal<Room | null>(null);
  bookingForm!: FormGroup;
  isSubmitting = signal<boolean>(false);
  customerId = signal<string>('');
  customerEmail = signal<string>('');
  customerName = signal<string>('');
  totalPrice = signal<number>(0);
  numberOfNights = signal<number>(0);

  constructor(private emailService: EmailService) {}

  ngOnInit(): void {
    
    this.authService.currentUser$.pipe(takeUntilDestroyed(this.destroyRef)).subscribe(user => {
      if (user && user.id) {
        this.customerId.set(user.id);
        this.customerEmail.set(user.email || '');
        this.customerName.set(user.email?.split('@')[0] || 'Customer'); // Use email prefix as name
        console.log('Current user loaded:', { id: user.id, email: user.email, role: user.role });
      } else {
        console.log('No user found, redirecting to login');
        this.router.navigate(['/user-login']);
      }
    });


    this.route.queryParams.pipe(takeUntilDestroyed(this.destroyRef)).subscribe(params => {
      const roomId = params['roomId'];
      const hotelId = params['hotelId'];
      const checkIn = params['checkIn'];
      const checkOut = params['checkOut'];

      if (roomId && hotelId) {
     
        this.initializeForm(checkIn, checkOut);
        
        this.loadRoomDetails(roomId);
      } else {
        this.router.navigate(['/hotel']);
      }
    });
  }

  initializeForm(checkIn?: string, checkOut?: string): void {
    this.bookingForm = this.fb.group({
      checkIn: [checkIn || '', [Validators.required, minDateTodayValidator()]],
      checkOut: [checkOut || '', [Validators.required, checkOutAfterCheckInValidator()]]
    });
    
    // Re-validate checkOut when checkIn changes
    this.bookingForm.get('checkIn')?.valueChanges.subscribe(() => {
      this.bookingForm.get('checkOut')?.updateValueAndValidity();
      this.calculateTotal();
    });

    // Calculate total when checkOut changes
    this.bookingForm.get('checkOut')?.valueChanges.subscribe(() => {
      this.calculateTotal();
    });

    // Initial validation and calculation if dates are provided
    if (checkIn && checkOut) {
      // Trigger validation on pre-filled dates
      this.bookingForm.get('checkIn')?.updateValueAndValidity();
      this.bookingForm.get('checkOut')?.updateValueAndValidity();
      this.calculateTotal();
    }
  }

  loadRoomDetails( roomId: string): void {
    this.roomService.getRoom(roomId).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (room) => {
        if (room) {
          this.room.set(room);
          // Trigger calculation after room is loaded
          // This ensures the calculation runs even with prefilled dates
          this.calculateTotal();
        } else {
          this.toast.error('Room not found');
          this.goBack();
        }
      },
      error: (err) => {
        console.error('Error loading room:', err);
        this.toast.error('Failed to load room details');
        this.goBack();
      }
    });
  }

  calculateTotal(): void {
    const checkIn = this.bookingForm.get('checkIn')?.value;
    const checkOut = this.bookingForm.get('checkOut')?.value;
    const room = this.room();

    console.log('Calculating total:', { checkIn, checkOut, room: room?.id, pricePerNight: room?.pricePerNight });

    if (checkIn && checkOut && room) {
      const checkInDate = new Date(checkIn);
      const checkOutDate = new Date(checkOut);
      const diffTime = checkOutDate.getTime() - checkInDate.getTime();
      const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

      if (diffDays > 0) {
        this.numberOfNights.set(diffDays);
        this.totalPrice.set(diffDays * room.pricePerNight);
        console.log('Calculation result:', { nights: diffDays, total: diffDays * room.pricePerNight });
      } else {
        this.numberOfNights.set(0);
        this.totalPrice.set(0);
        console.log('Invalid date range: diffDays =', diffDays);
      }
    } else {
      console.log('Missing data for calculation:', { 
        hasCheckIn: !!checkIn, 
        hasCheckOut: !!checkOut, 
        hasRoom: !!room 
      });
    }
  }

  onSubmit(): void {
    
    
    if (this.bookingForm.invalid) {
      this.bookingForm.markAllAsTouched();
      this.toast.error('Please fix the form errors before submitting');
      return;
    }

    const room = this.room();
    if (!room || !this.customerId()) {
      this.toast.error('Missing booking information');
      console.log('Missing data:', { hasRoom: !!room, customerId: this.customerId() });
      return;
    }

    this.isSubmitting.set(true);
    const checkInValue = this.bookingForm.get('checkIn')?.value;
    const checkOutValue = this.bookingForm.get('checkOut')?.value;

    // Create booking first
    const bookingCommand: CreateBookingCommand = {
      customerId: this.customerId(),
      roomId: room.id,
      checkInDate: checkInValue, // Backend expects YYYY-MM-DD format from date input
      checkOutDate: checkOutValue,
      totalAmount: this.totalPrice()
    };
    console.log('Creating booking with command:', bookingCommand);
    this.bookingService.createBooking(bookingCommand).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (response: BookingResponse) => {
        // Booking created, backend returns BookingResponse with Razorpay order details
        console.log('Booking created successfully:', response);
        this.initiateRazorpayPayment(response, room);
      },
      error: (err) => {
        this.isSubmitting.set(false);
        console.error('=== BOOKING ERROR ===');
        console.error('Full error object:', err);
        console.error('Error details:', {
          status: err.status,
          statusText: err.statusText,
          message: err.error?.message || err.message,
          details: err.error?.details || err.error,
          url: err.url,
          headers: err.headers
        });
        const errorMessage = err.error?.message || err.error?.details || err.message || 'Failed to create booking';
        this.toast.error('Failed to create booking: ' + errorMessage);
      }
    });
  }

  initiateRazorpayPayment(bookingResponse: BookingResponse, room: Room): void {
    console.log('Initiating payment for booking:', bookingResponse.bookingId);
    
    const options = {
      key:razorpayKey , // Using key from backend response
      amount: bookingResponse.amount , // Amount in paise from backend
      currency: 'INR',
      name: 'Hotel Booking',
      description: `${room.roomType.typeName} - Room ${room.roomNumber}`,
      order_id: bookingResponse.razorpayOrderId, // Razorpay order ID from backend
      handler: (response: any) => {
        console.log('Razorpay payment response:', response);
        
        // Payment successful - now verify with backend
        const verification: PaymentVerification = {
          bookingId: bookingResponse.bookingId,
          paymentId: response.razorpay_payment_id,
          orderId: response.razorpay_order_id,
          signature: response.razorpay_signature
        };
        
        console.log('Verifying payment with data:', verification);
        
        // Verify payment with backend
        this.paymentService.verifyPayment(verification).pipe(
          takeUntilDestroyed(this.destroyRef),
          switchMap(() => {
            const emailData = {
                to: this.customerEmail(),
                subject: `Booking Confirmed! - Order #${bookingResponse.bookingId}`,
                  body: `
                    <h1>Booking Confirmed</h1>
                    <p>Dear ${this.customerName()},</p>
                    <p>Your booking for <strong>${room.roomType.typeName}</strong> (Room ${room.roomNumber}) is confirmed.</p>
                    <p><strong>Check-in:</strong> ${this.bookingForm.get('checkIn')?.value}</p>
                    <p><strong>Amount Paid:</strong> ₹${this.totalPrice()}</p>
                  `
        };

        return this.emailService.sendEmail(emailData).pipe(
        // If email fails, catch it so we don't stop the user from seeing the success page
        catchError(emailErr => {
          console.error('Email failed to send:', emailErr);
          this.toast.warning('Booking successful, but failed to send confirmation email.');
          return of(null); // Return null to keep the stream alive
        })
      );
          }),
          catchError((err) => {
            console.error('Payment verification error:', err);
            throw err;
          })
        ).subscribe({
          next: (verificationResponse) => {
            this.isSubmitting.set(false);
            this.toast.success('Payment verified successfully!');
            this.router.navigate(['/payment-success'], {
              queryParams: {
                bookingId: bookingResponse.bookingId,
                paymentId: response.razorpay_payment_id
              }
            });
          },
          error: (err) => {
            this.isSubmitting.set(false);
            this.toast.error('Payment verification failed');
          }
        });
      },
      prefill: {
        name: this.customerName(),
        email: this.customerEmail(),
        contact: ''
      },
      notes: {
        bookingId: bookingResponse.bookingId,
        roomId: room.id
      },
      theme: {
        color: '#667eea'
      },
      modal: {
        ondismiss: () => {
          // Payment cancelled
          
          this.isSubmitting.set(false);
          this.bookingService.updateBooking(bookingResponse.bookingId,{status: "Cancelled"}).subscribe();
          this.toast.error('Payment cancelled');
          this.router.navigate(['/payment-failure'], {
            queryParams: {
              error: 'Payment was cancelled by user',
              bookingId: bookingResponse.bookingId
            }
          });
        }
      }
    };

    const razorpay = new Razorpay(options);
    
    razorpay.on('payment.failed', (response: any) => {
      // Payment failed
      console.log('Razorpay payment failed:', response);
      this.bookingService.updateBooking(bookingResponse.bookingId,{status: "Cancelled"}).subscribe();
      this.isSubmitting.set(false);
      this.toast.error('Payment failed');
      this.router.navigate(['/payment-failure'], {
        queryParams: {
          error: response.error.description,
          bookingId: bookingResponse.bookingId
        }
      });
    });

    razorpay.open();
  }

  goBack(): void {
    this.location.back();
  }
}
