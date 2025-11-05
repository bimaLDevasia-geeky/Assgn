using System;
using appDomain = Hotel.Booking.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Booking.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<appDomain.Customer?> GetByIdAsync(Guid id);
        Task<List<appDomain.Customer>> GetAllAsync();
        Task AddAsync(appDomain.Customer customer);
        void Delete(appDomain.Customer customer);
    }
}
