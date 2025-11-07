using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Booking
{
    public class GetBookingByIdQuery : IRequest<appDomain.Booking?>
    {
        public Guid Id { get; set; }
    }
}
