using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Interfaces;

public interface IEmployeeQueryService
{
    Task<appDomain.Employee?> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<appDomain.Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken = default);

    Task<appDomain.Employee?> GetEmployeeByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<List<appDomain.Employee>> GetEmployeeByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default);
}
