using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.Interfaces
{
    public interface IPayment
    {
        Task<Payment> CreatePayment(Payment payment);
        Task<Payment> GetPaymentById(int id);
        Task<IEnumerable<Payment>> GetAllPayments();
        Task<IEnumerable<Payment>> GetPaymentsByBooking(int bookingId);
        Task<Payment> UpdatePaymentStatus(int id, PaymentStatus status);
        Task<Payment> UpdatePayment(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentsByDateRange(DateTime startDate, DateTime endDate);
    }
}