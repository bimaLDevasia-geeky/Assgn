using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Interfaces;

public interface IReviewQueryService
{
    Task<appDomain.Review?> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<appDomain.Review>> GetAllReviewsAsync(CancellationToken cancellationToken = default);
    Task<List<appDomain.Review>> GetAllReviewsOfHotelAsync(Guid hotelId, CancellationToken cancellationToken = default);
}
