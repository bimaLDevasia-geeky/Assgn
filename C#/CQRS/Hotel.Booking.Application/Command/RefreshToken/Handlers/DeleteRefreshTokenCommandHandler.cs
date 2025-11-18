using Hotel.Booking.Application.Command.RefreshToken.Commands;
using Hotel.Booking.Domain.Interfaces;
using MediatR;

namespace Hotel.Booking.Application.Command.RefreshToken.Handlers;

public class DeleteRefreshTokenCommandHandler : IRequestHandler<DeleteRefreshTokenCommand, bool>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(request.Token, cancellationToken);

        if (token == null)
        {
            return false;
        }

        _refreshTokenRepository.Delete(token);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
