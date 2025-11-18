using Hotel.Booking.Domain.Interfaces;
using MediatR;
using System;

namespace Hotel.Booking.Application.Command.Payment.Commands
{
    public class DeletePaymentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
