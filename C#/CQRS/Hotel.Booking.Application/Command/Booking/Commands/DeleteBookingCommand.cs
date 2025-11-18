using Hotel.Booking.Domain.Interfaces;
using MediatR;
using System;

namespace Hotel.Booking.Application.Command.Booking.Commands
{
    public class DeleteBookingCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
