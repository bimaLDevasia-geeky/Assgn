using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query
{
    public class GetAllBookingsQuery : IRequest<List<appDomain.Booking>>
    {
    }
}
