using Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Booking.Infrastructure.Repository;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly HotelBookingDbContext _context;

    public RefreshTokenRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {   
        return await _context.RefreshTokens
            .Include(rt => rt.Employee)
            .Include(rt => rt.Customer)
            .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
    }

    public async Task<List<RefreshToken>> GetActiveTokensByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens
            .Where(rt => rt.EmployeeId == employeeId && !rt.IsRevoked && !rt.IsExpired)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
    }

    public void Delete(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Remove(refreshToken);
    }

    public void RevokeToken(RefreshToken refreshToken)
    {
        refreshToken.IsRevoked = true;
        _context.RefreshTokens.Update(refreshToken);
    }

    public async Task RevokeAllEmployeeTokensAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        var tokens = await _context.RefreshTokens
            .Where(rt => rt.EmployeeId == employeeId && !rt.IsRevoked)
            .ToListAsync(cancellationToken);

        foreach (var token in tokens)
        {
            token.IsRevoked = true;
        }

        _context.RefreshTokens.UpdateRange(tokens);
    }
}
