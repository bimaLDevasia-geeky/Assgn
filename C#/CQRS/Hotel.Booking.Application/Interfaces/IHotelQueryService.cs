using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Interfaces;

public interface IHotelQueryService
{
    Task<appDomain.Hotel?> GetHotelByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<appDomain.Hotel>> GetAllHotelsAsync(CancellationToken cancellationToken = default);

    Task<IQueryable<appDomain.Hotel>> GetHotelsAsQuerable( CancellationToken cancellationToken = default);
    
    Task<List<appDomain.Hotel>> ExecuteHotelQueryAsync(IQueryable<appDomain.Hotel> query, CancellationToken cancellationToken = default);
}
