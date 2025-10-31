using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.Interfaces
{
    public interface IBooking
    {
        Task<Booking> CreateBooking(Booking booking);
        Task<Booking> GetBookingById(int id);
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<IEnumerable<Booking>> GetBookingsByCustomer(int customerId);
        Task<IEnumerable<Booking>> GetBookingsByHotel(int hotelId);
        Task<Booking> UpdateBookingStatus(int id, BookingStatus status);
        Task<Booking> UpdateBooking(Booking booking);
        Task CancelBooking(int id);
    }
}