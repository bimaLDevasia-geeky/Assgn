using System;
using appDomain = Hotel.Booking.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Booking.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<appDomain.Room?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<appDomain.Room>> GetAllAsync();
        Task AddAsync(appDomain.Room room);
        void Delete(appDomain.Room room);
    }
}
