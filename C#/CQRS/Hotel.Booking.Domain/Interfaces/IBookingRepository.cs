using System;
using appDomain = Hotel.Booking.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Booking.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<appDomain.Booking?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<appDomain.Booking>> GetAllAsync();
        Task AddAsync(appDomain.Booking booking);
        void Delete(appDomain.Booking booking);
    }
}
