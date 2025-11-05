using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Booking.Infrastructure.Repository
{
    public class PaymentRepository(HotelBookingDbContext _context) : IPaymentRepository
    {
        public async Task<domain.Payment?> GetByIdAsync(Guid id)
        {
            return await _context.Payments
                .Include(p => p.Booking)
                    .ThenInclude(b => b.Customer)
                .Include(p => p.Booking)
                    .ThenInclude(b => b.Room)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<domain.Payment>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.Booking)
                    .ThenInclude(b => b.Customer)
                .Include(p => p.Booking)
                    .ThenInclude(b => b.Room)
                .ToListAsync();
        }

        public async Task AddAsync(domain.Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public void Delete(domain.Payment payment)
        {
            _context.Payments.Remove(payment);
        }
    }
}
