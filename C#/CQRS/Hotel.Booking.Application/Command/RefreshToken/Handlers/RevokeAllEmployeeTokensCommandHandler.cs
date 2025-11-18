using Hotel.Booking.Application.Command.RefreshToken.Commands;
using Hotel.Booking.Domain.Interfaces;
using MediatR;

namespace Hotel.Booking.Application.Command.RefreshToken.Handlers;

public class RevokeAllEmployeeTokensCommandHandler : IRequestHandler<RevokeAllEmployeeTokensCommand, bool>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RevokeAllEmployeeTokensCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RevokeAllEmployeeTokensCommand request, CancellationToken cancellationToken)
    {
        await _refreshTokenRepository.RevokeAllEmployeeTokensAsync(request.EmployeeId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
