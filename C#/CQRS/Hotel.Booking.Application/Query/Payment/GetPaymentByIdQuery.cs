using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Payment
{
    public class GetPaymentByIdQuery : IRequest<appDomain.Payment?>
    {
        public Guid Id { get; set; }
    }
}
