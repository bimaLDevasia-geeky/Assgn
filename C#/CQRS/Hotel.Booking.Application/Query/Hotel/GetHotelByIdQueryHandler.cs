using System;
using Hotel.Booking.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using appdomain = Hotel.Booking.Domain.Entities;


namespace Hotel.Booking.Application.Query.Hotel;

public class GetHotelByIdQueryHandler(HotelBookingDbContext context):IRequestHandler<GetHotelByIdQuery,appdomain.Hotel>
{
    public async Task<appdomain.Hotel> Handle(GetHotelByIdQuery request,CancellationToken ct)
    {
        var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == request.Id, ct);
        if (hotel == null)
        {
            throw new Exception("Hotel not found");
        }
        return hotel;
    }

}
