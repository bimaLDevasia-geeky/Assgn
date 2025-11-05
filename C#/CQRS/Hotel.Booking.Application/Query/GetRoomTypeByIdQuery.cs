using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query
{
    public class GetRoomTypeByIdQuery : IRequest<appDomain.RoomType?>
    {
        public Guid Id { get; set; }
    }
}
