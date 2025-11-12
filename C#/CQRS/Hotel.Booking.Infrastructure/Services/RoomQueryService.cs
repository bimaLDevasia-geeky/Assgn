using Hotel.Booking.Application.Interfaces;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Infrastructure.Services;

public class RoomQueryService : IRoomQueryService
{
    private readonly HotelBookingDbContext _context;

    public RoomQueryService(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<appDomain.Room?> GetRoomByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<List<appDomain.Room>> GetAllRoomsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Rooms.ToListAsync(cancellationToken);
    }

    public async Task<List<appDomain.Room>> GetRoomsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .Where(r => r.HotelId == hotelId)
            .Include(r => r.RoomType)
            .ToListAsync(cancellationToken);
    }
}
