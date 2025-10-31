using HotelBookingSystem.DTO;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.Interfaces
{
    public interface IHotelServices
    {
        Task<IEnumerable<Hotel>> GetAllHotels();
        Task<Hotel> GetHotelById(int id);
        Task<Hotel> AddHotel(HotelRequest hotel);
        Task<Hotel> UpdateHotel(HotelRequest hotel,int id);
        Task<bool> DeleteHotel(int id);
    }
}
