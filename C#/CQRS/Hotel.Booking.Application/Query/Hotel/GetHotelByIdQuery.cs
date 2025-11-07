using System;
using MediatR;
using appdomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query;

public class GetHotelByIdQuery:IRequest<appdomain.Hotel>
{
    public Guid Id { get; set; }
}

    
