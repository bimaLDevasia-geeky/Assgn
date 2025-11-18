using System;
using System.Reflection.Metadata;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Command.Register;

public class AdminRegisterCommand:IRequest<appDomain.Employee>
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string Role { get; } = "Admin";
}
