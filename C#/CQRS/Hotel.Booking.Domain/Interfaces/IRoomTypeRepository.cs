using System;
using appDomain = Hotel.Booking.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Booking.Domain.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task<appDomain.RoomType?> GetByIdAsync(Guid id);
        Task<List<appDomain.RoomType>> GetAllAsync();
        Task AddAsync(appDomain.RoomType roomType);
        void Delete(appDomain.RoomType roomType);
    }
}
