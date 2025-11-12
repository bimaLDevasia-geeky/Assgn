using System;
using Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Interfaces;

public interface ITokenRepository
{
    string GenerateAccessToken(Guid employeeId,string email, string role);
    RefreshToken GenerateRefreshToken(Guid employeeId);
    void SetRefreshToken(RefreshToken refreshToken);
}
