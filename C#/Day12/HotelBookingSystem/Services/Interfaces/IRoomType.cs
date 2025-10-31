using HotelBookingSystem.DTO;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.Interfaces
{
    public interface IRoomType
    {
        Task<RoomType> AddRoomType(RoomTypeDTO roomType);
        Task RemoveRoomType(int id);
        Task<RoomType> GetRoomTypeById(int id);
        Task<IEnumerable<RoomType>> GetAllRoomTypes();
        Task<RoomType> UpdateRoomType (RoomType roomType);



    }
}
