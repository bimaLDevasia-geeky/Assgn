using HotelBookingSystem.DTO;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.Interfaces
{
    public interface IBooking
    {
        Task<BookingResponseDTO> CreateBooking(BookingCreateDTO booking);
        Task<BookingResponseDTO> GetBookingById(int id);
        Task<IEnumerable<BookingResponseDTO>> GetAllBookings();
        Task<IEnumerable<BookingResponseDTO>> GetBookingsByCustomer(int customerId);
       
        Task<BookingResponseDTO> UpdateBookingStatus(int id, BookingStatus status);
        Task<BookingResponseDTO> UpdateBooking(int id, BookingUpdateDTO booking);
        Task CancelBooking(int id);
    }
}