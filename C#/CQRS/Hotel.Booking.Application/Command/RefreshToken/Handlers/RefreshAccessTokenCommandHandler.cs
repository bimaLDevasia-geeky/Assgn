using Hotel.Booking.Application.Command.RefreshToken.Commands;
using Hotel.Booking.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Application.Interfaces;
using System.Security.Authentication;

namespace Hotel.Booking.Application.Command.RefreshToken.Handlers;

public class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, string>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    private readonly ITokenService _tokenService;

    public RefreshAccessTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        string refreshToken=request.RefreshToken;
        
        // GetByTokenAsync already includes Employee and Customer navigation properties
        appDomain.RefreshToken? dbToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken);

        if(dbToken is null || dbToken.IsRevoked || dbToken.IsExpired)
        {
            throw new UnauthorizedAccessException("Invalid refresh token.");
        }

        // Revoke the old refresh token
        _refreshTokenRepository.RevokeToken(dbToken);
        
        // Generate new refresh token for either employee or customer
        appDomain.RefreshToken newRefreshToken = _tokenService.GenerateRefreshToken(
            employeeId: dbToken.EmployeeId,
            customerId: dbToken.CustomerId
        );

        // Add new refresh token to database
        await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
        
        // Save all changes (revoked old token + new token)
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Set the new refresh token in HTTP response cookie
        _tokenService.SetRefreshToken(newRefreshToken);

        // Generate access token based on whether it's an employee or customer
       if (dbToken.EmployeeId.HasValue)
    {
        // SAFETY CHECK: Ensure the navigation property is actually loaded
        if (dbToken.Employee == null) 
        {
            throw new Exception("Employee record associated with this token could not be found.");
        }

        return _tokenService.GenerateAccessToken(
            dbToken.EmployeeId.Value, 
            dbToken.Employee.Email, // This was likely causing the crash
            dbToken.Employee.Role
        );
    }
    else if (dbToken.CustomerId.HasValue)
    {
        // SAFETY CHECK: Ensure the navigation property is actually loaded
        if (dbToken.Customer == null)
        {
             throw new Exception("Customer record associated with this token could not be found.");
        }

        return _tokenService.GenerateAccessToken(
            dbToken.CustomerId.Value, 
            dbToken.Customer.Email, // This was likely causing the crash
            "Customer"
        );
    }
        else
        {
            throw new InvalidOperationException("Refresh token is not associated with an employee or customer.");
        }
    }
}
