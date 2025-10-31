using HotelBookingSystem.Context;
using HotelBookingSystem.DTO;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class RoomTypeServices:IRoomType
    {
        private readonly HotelBookingSystemContext _context;
        public RoomTypeServices(HotelBookingSystemContext context)
        {
            _context = context;
        }

        public async Task<RoomType> AddRoomType(RoomTypeDTO body)
        {
            RoomType roomType = new RoomType()
            {
                TypeName = body.TypeName,
                Capacity = body.Capacity,
                Description = body.Description
            };
            await _context.RoomTypes.AddAsync(roomType);
            await _context.SaveChangesAsync();
            return roomType;
        }

        public async Task<IEnumerable<RoomType>> GetAllRoomTypes()
        {
            return await _context.RoomTypes.ToListAsync();
        }

        public async Task<RoomType> GetRoomTypeById(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType == null)
                throw new KeyNotFoundException("RoomType not found");
            return roomType;
        }

        public async Task RemoveRoomType(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType == null)
                throw new KeyNotFoundException("RoomType not found");
            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();
        }

        public async Task<RoomType> UpdateRoomType(RoomType roomType)
        {
            var existingRoomType = await _context.RoomTypes.FindAsync(roomType.Id);
            if (existingRoomType == null)
                throw new KeyNotFoundException("RoomType not found");

            existingRoomType.TypeName = roomType.TypeName;
            existingRoomType.Description = roomType.Description;
            existingRoomType.Capacity = roomType.Capacity;

            await _context.SaveChangesAsync();
            return existingRoomType;
        }
    }
}
