using System;

namespace Hotel.Booking.Domain.Entities;

public class Customer
{
    public  Guid Id { get; private set; }
    public string FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public string IdProofNumber { get; private set; } = null!;
    public ICollection<Booking> Bookings { get; private set; } = new List<Booking>();

    public static Customer Create(string fullName, string email, string phoneNumber, string idProofNumber)
    {
        return new Customer
        {
            FullName = fullName,
            Email = email,
            PhoneNumber = phoneNumber,
            IdProofNumber = idProofNumber
        };
    }

    public void UpdateDetails(string? fullName, string? email, string? phoneNumber, string? idProofNumber)
    {
        if (!string.IsNullOrWhiteSpace(fullName))
        {
            FullName = fullName;
        }
        if (!string.IsNullOrWhiteSpace(email))
        {
            Email = email;
        }
        if (!string.IsNullOrWhiteSpace(phoneNumber))
        {
            PhoneNumber = phoneNumber;
        }
        if (!string.IsNullOrWhiteSpace(idProofNumber))
        {
            IdProofNumber = idProofNumber;
        }
    }
}
