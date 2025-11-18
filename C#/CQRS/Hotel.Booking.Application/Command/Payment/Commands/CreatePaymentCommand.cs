using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

namespace Hotel.Booking.Application.Command.Payment.Commands
{
    public class CreatePaymentCommand : IRequest<Guid>
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }=null!;
    }
}
