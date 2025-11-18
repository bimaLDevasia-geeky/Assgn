using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Interfaces;

public interface ICustomerQueryService
{
    Task<appDomain.Customer?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<appDomain.Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default);
    Task<appDomain.Customer?> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken = default);
}
