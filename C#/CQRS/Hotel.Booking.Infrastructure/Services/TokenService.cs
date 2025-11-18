using System;
using System.Security.Claims;
using Hotel.Booking.Application.Interfaces;
using Hotel.Booking.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace Hotel.Booking.Infrastructure.Repository;

public class TokenService:ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    }
    
    public string GenerateAccessToken(Guid employeeId, string email, string role)
    {
        List<Claim> claims= new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, employeeId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

        SigningCredentials cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
        DateTime expiration;
        string? accessTokenExpiration = _configuration["Jwt:AccessTokenExpirationInMinutes"];
        if (int.TryParse(accessTokenExpiration, out int accessTokenExpirationMinutes))
        {
            expiration = DateTime.UtcNow.AddMinutes(accessTokenExpirationMinutes);
        }
        else
        {
            throw new Exception("Invalid access token expiration configuration.");
        }

        // Generate the token using the claims
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: cred
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken(Guid? employeeId = null, Guid? customerId = null)
    {
        byte[] randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        string? time = _configuration["Jwt:RefreshTokenExpirationInDays"];
        if (int.TryParse(time, out int refreshTokenExpirationDays))
        {
            return new RefreshToken
            {
                EmployeeId = employeeId,
                CustomerId = customerId,
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(refreshTokenExpirationDays),
                Created = DateTime.UtcNow
            };
        }
        else
        {
            throw new Exception("Invalid refresh token expiration configuration.");
        }
    }

    public void SetRefreshToken(RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires,
            // Secure = true, // for production
            // SameSite = SameSiteMode.Strict // for production

        };
        _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }
    
}
