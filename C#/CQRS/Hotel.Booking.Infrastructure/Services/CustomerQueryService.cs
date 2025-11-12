using Hotel.Booking.Application.Interfaces;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Infrastructure.Services;

public class CustomerQueryService : ICustomerQueryService
{
    private readonly HotelBookingDbContext _context;

    public CustomerQueryService(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<appDomain.Customer?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<List<appDomain.Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Customers.ToListAsync(cancellationToken);
    }
}
