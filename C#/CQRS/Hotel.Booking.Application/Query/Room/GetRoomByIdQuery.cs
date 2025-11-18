using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Room
{
    public class GetRoomByIdQuery : IRequest<appDomain.Room?>
    {
        public Guid Id { get; set; }
    }
}
