using System;
using Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Guid employeeId,string email, string role);
    RefreshToken GenerateRefreshToken(Guid? employeeId = null, Guid? customerId = null);
    void SetRefreshToken(RefreshToken refreshToken);

    
}
