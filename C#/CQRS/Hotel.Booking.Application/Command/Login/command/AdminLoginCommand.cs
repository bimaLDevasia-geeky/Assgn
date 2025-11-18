using System;
using Hotel.Booking.Application.DTOs;
using MediatR;

namespace Hotel.Booking.Application.Command.Login.command;

public class AdminLoginCommand:IRequest<AdminResponse>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
