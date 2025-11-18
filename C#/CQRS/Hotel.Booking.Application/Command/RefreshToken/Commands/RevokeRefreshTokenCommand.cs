using MediatR;

namespace Hotel.Booking.Application.Command.RefreshToken.Commands;

public class RevokeRefreshTokenCommand : IRequest<bool>
{
    public string Token { get; set; } = string.Empty;
}
