using System;
using appDomain = Hotel.Booking.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Booking.Domain.Interfaces
{
    public interface IHotelRepository
    {
        Task<appDomain.Hotel?> GetByIdAsync(Guid Id);
        Task AddAsync(appDomain.Hotel hotel);

        void Delete(appDomain.Hotel hotel);


        
    }
}
