using Hotel.Booking.Application.Interfaces;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Infrastructure.Services;

public class PaymentQueryService : IPaymentQueryService
{
    private readonly HotelBookingDbContext _context;

    public PaymentQueryService(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<appDomain.Payment?> GetPaymentByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Payments.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<appDomain.Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Payments.ToListAsync(cancellationToken);
    }
}
