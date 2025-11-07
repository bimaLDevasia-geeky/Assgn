using System;
using appDomain = Hotel.Booking.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Booking.Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task<appDomain.Review?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<appDomain.Review>> GetAllAsync();
        Task AddAsync(appDomain.Review review);
        void Delete(appDomain.Review review);
    }
}
