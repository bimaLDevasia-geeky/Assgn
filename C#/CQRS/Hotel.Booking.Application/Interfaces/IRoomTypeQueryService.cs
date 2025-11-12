using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Interfaces;

public interface IRoomTypeQueryService
{
    Task<appDomain.RoomType?> GetRoomTypeByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<appDomain.RoomType>> GetAllRoomTypesAsync(CancellationToken cancellationToken = default);
}
