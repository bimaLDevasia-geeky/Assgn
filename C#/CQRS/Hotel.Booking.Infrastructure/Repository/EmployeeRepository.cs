using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Booking.Infrastructure.Repository
{
    public class EmployeeRepository(HotelBookingDbContext _context) : IEmployeeRepository
    {
        public async Task<domain.Employee?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Employees
                .Include(e => e.Hotel)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<domain.Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Hotel)
                .ToListAsync();
        }

        public async Task AddAsync(domain.Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public void Delete(domain.Employee employee)
        {
            _context.Employees.Remove(employee);
        }
    }
}
