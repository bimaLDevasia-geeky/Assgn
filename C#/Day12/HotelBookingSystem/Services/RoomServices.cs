using HotelBookingSystem.Context;
using HotelBookingSystem.DTO;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class RoomServices : IRoom
    {
        private readonly HotelBookingSystemContext _context;

        public RoomServices(HotelBookingSystemContext context)
        {
            _context = context;
        }

        public async Task<RoomResponseDTO> AddRoom(RoomCreateDTO roomDto)
        {
            var room = new Room
            {
                RoomNumber = roomDto.RoomNumber,
                PricePerNight = roomDto.PricePerNight,
                RoomTypeId = roomDto.RoomTypeId,
                HotelId = roomDto.HotelId,
                Status = RoomStatus.Available
            };

            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();

          
            return CreateRoomResponseDTO(room);
        }

        public async Task<IEnumerable<RoomResponseDTO>> GetAllRooms()
        {
            var rooms = await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Hotel)
                .ToListAsync();

            return rooms.Select(room => CreateRoomResponseDTO(room));
        }

        public async Task<RoomResponseDTO> GetRoomById(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
                throw new KeyNotFoundException("Room not found");

            return CreateRoomResponseDTO(room);
        }

        public async Task<IEnumerable<RoomResponseDTO>> GetRoomsByHotel(int hotelId)
        {
            var rooms = await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Hotel)
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();

            return rooms.Select(room => CreateRoomResponseDTO(room));
        }

                

        private RoomResponseDTO CreateRoomResponseDTO(Room room)
        {
            return new RoomResponseDTO
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Status = room.Status.ToString(),
                PricePerNight = room.PricePerNight,
                RoomType = new RoomTypeResponseDTO
                {
                    Id = room.RoomType.Id,
                    TypeName = room.RoomType.TypeName,
                    Description = room.RoomType.Description,
                    Capacity = room.RoomType.Capacity
                },
                Hotel = new HotelResponseDTO
                {
                    Id = room.Hotel.Id,
                    Name = room.Hotel.Name,
                    Address = room.Hotel.Address,
                    City = room.Hotel.City,
                    Country = room.Hotel.Country,
                    PhoneNumber = room.Hotel.PhoneNumber
                }
            };
        }

        public async Task<IEnumerable<RoomResponseDTO>> GetAvailableRooms(DateTime checkIn, DateTime checkOut, int hotelId)
        {
            List<Room> rooms = await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Hotel)
                .Include(r => r.Bookings)
                .Where(r => r.HotelId == hotelId && 
                    r.Status == RoomStatus.Available &&
                    !r.Bookings.Any(b => 
                        (checkIn <= b.CheckOutTime && checkOut >= b.CheckInTime) &&
                        b.Status != BookingStatus.Cancelled))
                .ToListAsync();

            return rooms.Select(room => CreateRoomResponseDTO(room));
        }

        public async Task RemoveRoom(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            if (room is null)
                throw new KeyNotFoundException("Room not found");

            // Check if room has any active bookings
            var hasActiveBookings = await _context.Bookings
                .AnyAsync(b => b.RoomId == id && 
                              b.Status != BookingStatus.Cancelled && 
                              b.CheckOutTime > DateTime.Now);

            if (hasActiveBookings)
                throw new InvalidOperationException("Cannot delete room with active bookings");

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<RoomResponseDTO> UpdateRoom(int id, RoomUpdateDTO roomDto)
        {
            Room room = await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room is null)
                throw new KeyNotFoundException("Room not found");

            room.RoomNumber = roomDto.RoomNumber;
            room.PricePerNight = roomDto.PricePerNight;
            room.RoomTypeId = roomDto.RoomTypeId;
            room.Status = Enum.Parse<RoomStatus>(roomDto.Status);

            await _context.SaveChangesAsync();
            return CreateRoomResponseDTO(room);
        }

        public async Task<RoomResponseDTO> UpdateRoomStatus(int id, RoomStatus status)
        {
            Room room = await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room is null)
                throw new KeyNotFoundException("Room not found");

            room.Status = status;
            await _context.SaveChangesAsync();
            return CreateRoomResponseDTO(room);
        }
    }
}