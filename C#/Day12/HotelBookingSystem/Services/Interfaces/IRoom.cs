using HotelBookingSystem.DTO;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.Interfaces
{
    public interface IRoom
    {
        Task<RoomResponseDTO> AddRoom(RoomCreateDTO room);
        Task<RoomResponseDTO> GetRoomById(int id);
        Task<IEnumerable<RoomResponseDTO>> GetAllRooms();
        Task<IEnumerable<RoomResponseDTO>> GetRoomsByHotel(int hotelId);
        Task<IEnumerable<RoomResponseDTO>> GetAvailableRooms(DateTime checkIn, DateTime checkOut, int hotelId);
        Task<RoomResponseDTO> UpdateRoom(int id, RoomUpdateDTO room);
        Task RemoveRoom(int id);
        Task<RoomResponseDTO> UpdateRoomStatus(int id, RoomStatus status);
    }
}