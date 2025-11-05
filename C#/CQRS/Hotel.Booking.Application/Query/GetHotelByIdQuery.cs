using System;
using MediatR;

namespace Hotel.Booking.Application.Query;

public class GetHotelByIdQuery:IRequest<Guid>
{
    public Guid Id { get; set; }
}

    
