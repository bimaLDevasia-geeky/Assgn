using System;

namespace Hotel.Booking.Domain.Entities;

public class Employee
{
    public Guid Id { get; private set; }
    public Guid HotelId { get; private set; }
    public string FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; private set; } = null!;
    public Hotel Hotel { get; private set; } = null!;

    public List<RefreshToken> RefreshTokens { get; set; } = new();

    public static Employee Create(string fullName, string email, string role, Guid hotelId)
    {
        return new Employee
        {
            FullName = fullName,
            Email = email,
            Role = role,
            HotelId = hotelId
        };
    }

    public void UpdateDetails(string? fullName, string? email, string? role)
    {
        if (!string.IsNullOrWhiteSpace(fullName))
        {
            FullName = fullName;
        }
        if (!string.IsNullOrWhiteSpace(email))
        {
            Email = email;
        }
        if (!string.IsNullOrWhiteSpace(role))
        {
            Role = role;
        }
        
    }
}
