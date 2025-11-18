using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Command.RefreshToken.Commands;

public class RefreshAccessTokenCommand : IRequest<string>
{
   public string RefreshToken { get; set; } = null!;
}
