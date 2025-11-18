using Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<List<RefreshToken>> GetActiveTokensByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
    void Delete(RefreshToken refreshToken);
    void RevokeToken(RefreshToken refreshToken);
    Task RevokeAllEmployeeTokensAsync(Guid employeeId, CancellationToken cancellationToken = default);
}
