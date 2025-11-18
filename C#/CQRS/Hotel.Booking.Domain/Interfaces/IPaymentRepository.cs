using System;
using appDomain = Hotel.Booking.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Booking.Domain.Interfaces
{
    public interface IPaymentRepository
    {
        Task<appDomain.Payment?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<appDomain.Payment>> GetAllAsync();
        Task AddAsync(appDomain.Payment payment);
        void Delete(appDomain.Payment payment);
    }
}
