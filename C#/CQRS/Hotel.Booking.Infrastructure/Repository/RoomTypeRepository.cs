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
    public class RoomTypeRepository(HotelBookingDbContext _context) : IRoomTypeRepository
    {
        public async Task<domain.RoomType?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.RoomTypes
                .Include(rt => rt.Rooms)
                .FirstOrDefaultAsync(rt => rt.Id == id);
        }

        public async Task<List<domain.RoomType>> GetAllAsync()
        {
            return await _context.RoomTypes
                .Include(rt => rt.Rooms)
                .ToListAsync();
        }

        public async Task AddAsync(domain.RoomType roomType)
        {
            await _context.RoomTypes.AddAsync(roomType);
        }

        public void Delete(domain.RoomType roomType)
        {
            _context.RoomTypes.Remove(roomType);
        }
    }
}
