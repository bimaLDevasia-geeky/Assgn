using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Room;

public class GetRoomsByHotelIdWithFiltersQuery : IRequest<List<appDomain.Room>>
{
    public Guid HotelId { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }

}
