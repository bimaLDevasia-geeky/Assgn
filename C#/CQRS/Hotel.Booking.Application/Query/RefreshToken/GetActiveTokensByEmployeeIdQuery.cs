using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.RefreshToken;

public class GetActiveTokensByEmployeeIdQuery : IRequest<List<appDomain.RefreshToken>>
{
    public Guid EmployeeId { get; set; }
}
