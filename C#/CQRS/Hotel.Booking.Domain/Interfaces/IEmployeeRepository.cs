using System;
using appDomain = Hotel.Booking.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Booking.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<appDomain.Employee?> GetByIdAsync(Guid id);
        Task<List<appDomain.Employee>> GetAllAsync();
        Task AddAsync(appDomain.Employee employee);
        void Delete(appDomain.Employee employee);
    }
}
