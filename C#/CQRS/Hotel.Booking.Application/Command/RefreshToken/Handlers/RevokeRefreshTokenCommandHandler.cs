using Hotel.Booking.Application.Command.RefreshToken.Commands;
using Hotel.Booking.Domain.Interfaces;
using MediatR;

namespace Hotel.Booking.Application.Command.RefreshToken.Handlers;

public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, bool>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RevokeRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(request.Token, cancellationToken);

        if (token == null || token.IsRevoked || token.IsExpired)
        {
            return false;
        }

        _refreshTokenRepository.RevokeToken(token);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
