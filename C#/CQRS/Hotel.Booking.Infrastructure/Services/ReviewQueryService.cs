using Hotel.Booking.Application.Interfaces;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Infrastructure.Services;

public class ReviewQueryService : IReviewQueryService
{
    private readonly HotelBookingDbContext _context;

    public ReviewQueryService(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<appDomain.Review?> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<List<appDomain.Review>> GetAllReviewsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Reviews.ToListAsync(cancellationToken);
    }

    public async Task<List<appDomain.Review>> GetAllReviewsOfHotelAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .Include(r => r.Customer)
            .Where(r => r.HotelId == hotelId)
            .ToListAsync(cancellationToken);
    }
}
