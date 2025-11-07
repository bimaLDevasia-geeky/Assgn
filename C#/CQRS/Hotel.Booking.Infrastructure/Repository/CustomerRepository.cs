using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Booking.Infrastructure.Repository
{
    public class CustomerRepository(HotelBookingDbContext _context) : ICustomerRepository
    {
        public async Task<domain.Customer?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Customers
                .Include(c => c.Bookings)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<domain.Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.Bookings)
                .ToListAsync();
        }

        public async Task AddAsync(domain.Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public void Delete(domain.Customer customer)
        {
            _context.Customers.Remove(customer);
        }
    }
}
