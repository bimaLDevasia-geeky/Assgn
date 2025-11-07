using System;
using MediatR;
namespace Hotel.Booking.Application.Command.Hotel;

public class DeleteHotelCommand:IRequest<bool>
{
    public Guid Id { get; set; }
}
