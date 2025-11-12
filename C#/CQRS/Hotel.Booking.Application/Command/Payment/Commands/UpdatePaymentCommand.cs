using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command.Payment.Commands
{
    public class UpdatePaymentCommand : IRequest<domain.Payment>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public domain.PaymentStatus? Status { get; set; }
        public decimal? Amount { get; set; }
        public domain.PaymentMethod? PaymentMethod { get; set; }
    }
}
