using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Booking.Infrastructure.Repository
{
    public class RoomRepository(HotelBookingDbContext _context) : IRoomRepository
    {
        public async Task<domain.Room?> GetByIdAsync(Guid id)
        {
            return await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<domain.Room>> GetAllAsync()
        {
            return await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .Include(r => r.Bookings)
                .ToListAsync();
        }

        public async Task AddAsync(domain.Room room)
        {
            await _context.Rooms.AddAsync(room);
        }

        public void Delete(domain.Room room)
        {
            _context.Rooms.Remove(room);
        }
    }
}
