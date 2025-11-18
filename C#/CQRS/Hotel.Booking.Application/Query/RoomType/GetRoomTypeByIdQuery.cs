using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.RoomType
{
    public class GetRoomTypeByIdQuery : IRequest<appDomain.RoomType?>
    {
        public Guid Id { get; set; }
    }
}
