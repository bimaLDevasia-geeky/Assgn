using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Interfaces;

public interface IBookingQueryService
{
    Task<appDomain.Booking?> GetBookingByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<appDomain.Booking>> GetAllBookingsAsync(CancellationToken cancellationToken = default);
}
