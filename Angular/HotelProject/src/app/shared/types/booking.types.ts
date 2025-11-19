export interface BookingResponse {
  bookingId: string;
  razorpayOrderId: string;
  amount: number;
  key: string; // The Public Key from backend
}

export interface PaymentVerification {
  bookingId: string;
  paymentId: string;
  orderId: string;
  signature: string;
}