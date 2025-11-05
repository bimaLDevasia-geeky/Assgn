using Hotel.Booking.Domain.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Booking.Infrastructure.Persistance;

namespace Hotel.Booking.Infrastructure.Repository
{
    public class HotelRepository(HotelBookingDbContext _context):IHotelRepository
    {
        public async Task<appDomain.Hotel?> GetByIdAsync(Guid id)
        {
            return await _context.Hotels.FindAsync(id);

        }

        public async Task AddAsync(appDomain.Hotel hotel)
        {
             await _context.AddAsync(hotel);
        }
        public void Delete(appDomain.Hotel hotel)
        {
            _context.Hotels.Remove(hotel);
        }
    }
}
