using MediatR;

namespace Hotel.Booking.Application.Command.RefreshToken.Commands;

public class DeleteRefreshTokenCommand : IRequest<bool>
{
    public string Token { get; set; } = string.Empty;
}
