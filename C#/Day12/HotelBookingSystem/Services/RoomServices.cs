using HotelBookingSystem.Context;
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

        public async Task<Room> AddRoom(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Hotel)
                .ToListAsync();
        }

        public async Task<Room> GetRoomById(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
                throw new KeyNotFoundException("Room not found");

            return room;
        }

        public async Task<IEnumerable<Room>> GetRoomsByHotel(int hotelId)
        {
            return await _context.Rooms
                .Include(r => r.RoomType)
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAvailableRooms(DateTime checkIn, DateTime checkOut, int hotelId)
        {
            return await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Bookings)
                .Where(r => r.HotelId == hotelId && 
                    r.Status == RoomStatus.Available &&
                    !r.Bookings.Any(b => 
                        (checkIn <= b.CheckOutTime && checkOut >= b.CheckInTime) &&
                        b.Status != BookingStatus.Cancelled))
                .ToListAsync();
        }

        public async Task RemoveRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                throw new KeyNotFoundException("Room not found");

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> UpdateRoom(Room room)
        {
            var existingRoom = await _context.Rooms.FindAsync(room.Id);
            if (existingRoom == null)
                throw new KeyNotFoundException("Room not found");

            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.Status = room.Status;
            existingRoom.PricePerNight = room.PricePerNight;
            existingRoom.RoomTypeId = room.RoomTypeId;
            existingRoom.HotelId = room.HotelId;

            await _context.SaveChangesAsync();
            return existingRoom;
        }

        public async Task<Room> UpdateRoomStatus(int id, RoomStatus status)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                throw new KeyNotFoundException("Room not found");

            room.Status = status;
            await _context.SaveChangesAsync();
            return room;
        }
    }
}