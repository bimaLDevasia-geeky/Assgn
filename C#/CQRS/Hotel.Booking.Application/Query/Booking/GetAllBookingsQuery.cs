using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Booking
{
    public class GetAllBookingsQuery : IRequest<List<appDomain.Booking>>
    {
    }
}
