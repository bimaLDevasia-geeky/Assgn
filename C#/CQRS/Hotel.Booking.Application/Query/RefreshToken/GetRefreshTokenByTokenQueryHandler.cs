using Hotel.Booking.Domain.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.RefreshToken;

public class GetRefreshTokenByTokenQueryHandler : IRequestHandler<GetRefreshTokenByTokenQuery, appDomain.RefreshToken?>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public GetRefreshTokenByTokenQueryHandler(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<appDomain.RefreshToken?> Handle(GetRefreshTokenByTokenQuery request, CancellationToken cancellationToken)
    {
        return await _refreshTokenRepository.GetByTokenAsync(request.Token, cancellationToken);
    }
}
