using Hotel.Booking.Application.Interfaces;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Infrastructure.Services;

public class HotelQueryService : IHotelQueryService
{
    private readonly HotelBookingDbContext _context;

    public HotelQueryService(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<appDomain.Hotel?> GetHotelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
    }

    public async Task<List<appDomain.Hotel>> GetAllHotelsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Hotels
            .Include(h => h.Rooms)
            .Include(h => h.Employees)
            .ToListAsync(cancellationToken);
    }

    public async Task<IQueryable<appDomain.Hotel>> GetHotelsAsQuerable(CancellationToken cancellationToken = default)
    {
        return _context.Hotels.Include(h=>h.Reviews).Include(h => h.Rooms).ThenInclude(r=>r.Bookings).AsQueryable();
    }

    public async Task<List<appDomain.Hotel>> ExecuteHotelQueryAsync(IQueryable<appDomain.Hotel> query, CancellationToken cancellationToken = default)
    {
        return await query.ToListAsync(cancellationToken);
    }
}
