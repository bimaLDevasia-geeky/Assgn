using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.RefreshToken;

public class GetRefreshTokenByTokenQuery : IRequest<appDomain.RefreshToken?>
{
    public string Token { get; set; } = string.Empty;
}
