using Hotel.Booking.Application.Interfaces;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Infrastructure.Services;

public class BookingQueryService : IBookingQueryService
{
    private readonly HotelBookingDbContext _context;

    public BookingQueryService(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<appDomain.Booking?> GetBookingByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<List<appDomain.Booking>> GetAllBookingsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Bookings.ToListAsync(cancellationToken);
    }
    public async Task<List<appDomain.Booking>> GetBookingsByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Bookings
            .Where(b => b.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }
}
