using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.RoomType
{
    public class GetAllRoomTypesQuery : IRequest<List<appDomain.RoomType>>
    {
    }
}
