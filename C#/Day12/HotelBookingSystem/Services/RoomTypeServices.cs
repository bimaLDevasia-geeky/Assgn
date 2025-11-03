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

        public async Task<RoomTypeResponseDTO> AddRoomType(RoomTypeCreateDTO body)
        {
            RoomType roomType = new RoomType()
            {
                TypeName = body.TypeName,
                Capacity = body.Capacity,
                Description = body.Description
            };
            await _context.RoomTypes.AddAsync(roomType);
            await _context.SaveChangesAsync();
            
            return new RoomTypeResponseDTO
            {
                Id = roomType.Id,
                TypeName = roomType.TypeName,
                Capacity = roomType.Capacity,
                Description = roomType.Description
            };
        }

        public async Task<IEnumerable<RoomTypeResponseDTO>> GetAllRoomTypes()
        {
            List<RoomType> roomTypes = await _context.RoomTypes.ToListAsync();
            return roomTypes.Select(rt => new RoomTypeResponseDTO
            {
                Id = rt.Id,
                TypeName = rt.TypeName,
                Capacity = rt.Capacity,
                Description = rt.Description
            });
        }

        public async Task<RoomTypeResponseDTO> GetRoomTypeById(int id)
        {
            RoomType roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType is null)
                throw new KeyNotFoundException("RoomType not found");

            return new RoomTypeResponseDTO
            {
                Id = roomType.Id,
                TypeName = roomType.TypeName,
                Capacity = roomType.Capacity,
                Description = roomType.Description
            };
        }

        public async Task RemoveRoomType(int id)
        {
            RoomType roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType is null)
                throw new KeyNotFoundException("RoomType not found");
            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();
        }

        public async Task<RoomTypeResponseDTO> UpdateRoomType(int id, RoomTypeUpdateDTO roomTypeDto)
        {
            RoomType existingRoomType = await _context.RoomTypes.FindAsync(id);
            if (existingRoomType is null)
                throw new KeyNotFoundException("RoomType not found");

            existingRoomType.TypeName = roomTypeDto.TypeName;
            existingRoomType.Description = roomTypeDto.Description;
            existingRoomType.Capacity = roomTypeDto.Capacity;

            await _context.SaveChangesAsync();

            return new RoomTypeResponseDTO
            {
                Id = existingRoomType.Id,
                TypeName = existingRoomType.TypeName,
                Capacity = existingRoomType.Capacity,
                Description = existingRoomType.Description
            };
        }
    }
}
