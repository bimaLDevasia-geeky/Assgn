using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.Interfaces
{
    public interface IRoom
    {
        Task<Room> AddRoom(Room room);
        Task<Room> GetRoomById(int id);
        Task<IEnumerable<Room>> GetAllRooms();
        Task<IEnumerable<Room>> GetRoomsByHotel(int hotelId);
        Task<IEnumerable<Room>> GetAvailableRooms(DateTime checkIn, DateTime checkOut, int hotelId);
        Task<Room> UpdateRoom(Room room);
        Task RemoveRoom(int id);
        Task<Room> UpdateRoomStatus(int id, RoomStatus status);
    }
}