using HotelBookingSystem.Context;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class PaymentServices : IPayment
    {
        private readonly HotelBookingSystemContext _context;

        public PaymentServices(HotelBookingSystemContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            var booking = await _context.Bookings.FindAsync(payment.BookingId);
            if (booking == null)
                throw new KeyNotFoundException("Booking not found");

            payment.PaymentDate = DateTime.Now;
            payment.Status = PaymentStatus.Pending;

            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _context.Payments
                .Include(p => p.Booking)
                    .ThenInclude(b => b.Customer)
                .ToListAsync();
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Booking)
                    .ThenInclude(b => b.Customer)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
                throw new KeyNotFoundException("Payment not found");

            return payment;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByBooking(int bookingId)
        {
            return await _context.Payments
                .Include(p => p.Booking)
                    .ThenInclude(b => b.Customer)
                .Where(p => p.BookingId == bookingId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Payments
                .Include(p => p.Booking)
                    .ThenInclude(b => b.Customer)
                .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                .ToListAsync();
        }

        public async Task<Payment> UpdatePayment(Payment payment)
        {
            var existingPayment = await _context.Payments.FindAsync(payment.Id);
            if (existingPayment == null)
                throw new KeyNotFoundException("Payment not found");

            existingPayment.Amount = payment.Amount;
            existingPayment.Method = payment.Method;
            existingPayment.Status = payment.Status;

            await _context.SaveChangesAsync();
            return existingPayment;
        }

        public async Task<Payment> UpdatePaymentStatus(int id, PaymentStatus status)
        {
            var payment = await _context.Payments
                .Include(p => p.Booking)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
                throw new KeyNotFoundException("Payment not found");

            payment.Status = status;

            // Update booking status if payment is completed
            if (status == PaymentStatus.Completed && payment.Booking != null)
            {
                payment.Booking.Status = BookingStatus.Confirmed;
            }
            else if (status == PaymentStatus.Cancelled && payment.Booking != null)
            {
                payment.Booking.Status = BookingStatus.Cancelled;
            }

            await _context.SaveChangesAsync();
            return payment;
        }
    }
}