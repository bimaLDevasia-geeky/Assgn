using System;
using Hotel.Booking.Application.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;


namespace Hotel.Booking.Application.Query.Hotel;

public class GetHotelByIdQueryHandler(IHotelQueryService service):IRequestHandler<GetHotelByIdQuery,appDomain.Hotel>
{
    public async Task<appDomain.Hotel> Handle(GetHotelByIdQuery request,CancellationToken ct)
    {
        var hotel = await service.GetHotelByIdAsync(request.Id, ct);
        if (hotel == null)
        {
            throw new Exception("Hotel not found");
        }
        return hotel;
    }

}
