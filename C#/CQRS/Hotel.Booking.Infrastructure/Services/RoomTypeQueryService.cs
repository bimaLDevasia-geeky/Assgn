using Hotel.Booking.Application.Interfaces;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Infrastructure.Services;

public class RoomTypeQueryService : IRoomTypeQueryService
{
    private readonly HotelBookingDbContext _context;

    public RoomTypeQueryService(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<appDomain.RoomType?> GetRoomTypeByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.RoomTypes.FirstOrDefaultAsync(rt => rt.Id == id, cancellationToken);
    }

    public async Task<List<appDomain.RoomType>> GetAllRoomTypesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.RoomTypes.ToListAsync(cancellationToken);
    }
}
