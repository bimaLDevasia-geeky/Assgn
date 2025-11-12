using System;
using MediatR;
using Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application;

public class GetRoomsByHostelIdQuery : IRequest<List<Room>>
{
    public Guid HotelId { get; set; }
}
