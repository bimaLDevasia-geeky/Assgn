using HotelBookingSystem.Context;
using HotelBookingSystem.DTO;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class HotelServices : IHotelServices
    {
        private readonly HotelBookingSystemContext _context;

        public HotelServices(HotelBookingSystemContext context)
        {
            _context = context;
        }
        public async Task<Hotel> AddHotel(HotelRequest request)
        {
            Hotel hotel = new Hotel()
            {
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                PhoneNumber = request.PhoneNumber,
            };
           _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }
        public async Task<IEnumerable<Hotel>> GetAllHotels() {

            List<Hotel> Hotels = await _context.Hotels.ToListAsync();
            return Hotels;
        }

        
        public async Task<Hotel> GetHotelById(int id)
        {
            Hotel hotel= await _context.Hotels.FirstOrDefaultAsync(x=> x.Id ==id);
            return hotel;
        }


        public async Task<Hotel> UpdateHotel(HotelRequest hotel,int id)
        {

            Hotel hot = _context.Hotels.FirstOrDefault(z => z.Id == id);

            if (hot == null)
            {
                throw new KeyNotFoundException($"Hotel with {id} is not found");
            }
                
                Hotel hotel1 = new Hotel()
                {
                    Id=id,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    City = hotel.City,
                    Country = hotel.Country,
                    PhoneNumber = hotel.PhoneNumber,

                };

                _context.Hotels.Update(hotel1);
                await _context.SaveChangesAsync();
                return hotel1;

            

        }

        public async Task<bool> DeleteHotel(int id)
        {
            
            Hotel ht = await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);
            if (ht != null)
            {
                 _context.Hotels.Remove(ht);
                await _context.SaveChangesAsync();
                return true;
            }
            throw new KeyNotFoundException("Key is not found");
        }


        
    }
}
