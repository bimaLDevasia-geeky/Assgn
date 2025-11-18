using Hotel.Booking.Application.Interfaces;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Infrastructure.Services;

public class EmployeeQueryService : IEmployeeQueryService
{
    private readonly HotelBookingDbContext _context;

    public EmployeeQueryService(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<appDomain.Employee?> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<List<appDomain.Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Employees.ToListAsync(cancellationToken);
    }

    public async Task<appDomain.Employee?> GetEmployeeByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email, cancellationToken);
    }

    public async Task<List<appDomain.Employee>> GetEmployeeByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await _context.Employees.Where(e => e.HotelId == hotelId).ToListAsync(cancellationToken);
    }
}
