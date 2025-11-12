using System;
using Hotel.Booking.Application.Interfaces;
using MediatR;
using appdomain = Hotel.Booking.Domain.Entities;


namespace Hotel.Booking.Application.Query.Hotel;

public class GetHotelByIdQueryHandler(IHotelQueryService service):IRequestHandler<GetHotelByIdQuery,appdomain.Hotel>
{
    public async Task<appdomain.Hotel> Handle(GetHotelByIdQuery request,CancellationToken ct)
    {
        var hotel = await service.GetHotelByIdAsync(request.Id, ct);
        if (hotel == null)
        {
            throw new Exception("Hotel not found");
        }
        return hotel;
    }

}
