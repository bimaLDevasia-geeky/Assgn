using System;
using MediatR;

namespace Hotel.Booking.Application.Command.Email.Command;

public class EmailCommand:IRequest<bool>
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
