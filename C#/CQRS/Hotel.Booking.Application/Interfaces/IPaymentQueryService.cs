using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Interfaces;

public interface IPaymentQueryService
{
    Task<appDomain.Payment?> GetPaymentByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<appDomain.Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken = default);
}
