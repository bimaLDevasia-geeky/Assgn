using Hotel.Booking.Domain.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.RefreshToken;

public class GetActiveTokensByEmployeeIdQueryHandler : IRequestHandler<GetActiveTokensByEmployeeIdQuery, List<appDomain.RefreshToken>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public GetActiveTokensByEmployeeIdQueryHandler(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<List<appDomain.RefreshToken>> Handle(GetActiveTokensByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        return await _refreshTokenRepository.GetActiveTokensByEmployeeIdAsync(request.EmployeeId, cancellationToken);
    }
}
