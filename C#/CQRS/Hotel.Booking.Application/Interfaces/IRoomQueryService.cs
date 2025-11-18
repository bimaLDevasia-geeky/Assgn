using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Interfaces;

public interface IRoomQueryService
{
    Task<appDomain.Room?> GetRoomByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<appDomain.Room>> GetAllRoomsAsync(CancellationToken cancellationToken = default);
    Task<List<appDomain.Room>> GetRoomsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default);
}
