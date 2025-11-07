using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Booking.Infrastructure.Repository
{
    public class BookingRepository(HotelBookingDbContext _context) : IBookingRepository
    {
        public async Task<domain.Booking?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Include(b => b.Payment)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<domain.Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Include(b => b.Payment)
                .ToListAsync();
        }

        public async Task AddAsync(domain.Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
        }

        public void Delete(domain.Booking booking)
        {
            _context.Bookings.Remove(booking);
        }
    }
}
