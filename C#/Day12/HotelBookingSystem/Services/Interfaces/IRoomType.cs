using HotelBookingSystem.DTO;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.Interfaces
{
    public interface IRoomType
    {
        Task<RoomTypeResponseDTO> AddRoomType(RoomTypeCreateDTO roomType);
        Task RemoveRoomType(int id);
        Task<RoomTypeResponseDTO> GetRoomTypeById(int id);
        Task<IEnumerable<RoomTypeResponseDTO>> GetAllRoomTypes();
        Task<RoomTypeResponseDTO> UpdateRoomType(int id, RoomTypeUpdateDTO roomType);
    }
}
