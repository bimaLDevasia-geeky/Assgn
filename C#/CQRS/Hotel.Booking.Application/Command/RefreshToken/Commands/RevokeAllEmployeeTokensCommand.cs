using MediatR;

namespace Hotel.Booking.Application.Command.RefreshToken.Commands;

public class RevokeAllEmployeeTokensCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
}
