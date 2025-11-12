using System;
using Hotel.Booking.Application.Interfaces;
using MediatR;
using appDomain=Hotel.Booking.Domain.Entities;
namespace Hotel.Booking.Application.Query.Room;


public class GetRoomsByHostelIdQueryHandler(IRoomQueryService service):IRequestHandler<GetRoomsByHostelIdQuery, List<appDomain.Room>>
{
    public async Task<List<appDomain.Room>> Handle(GetRoomsByHostelIdQuery request, CancellationToken cancellationToken)
    {
        return await service.GetRoomsByHotelIdAsync(request.HotelId, cancellationToken);
    }
}
