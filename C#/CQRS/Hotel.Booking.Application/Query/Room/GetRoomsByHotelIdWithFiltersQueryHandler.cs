using System;
using System.Linq;
using Hotel.Booking.Application.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Room;

public class GetRoomsByHotelIdWithFiltersQueryHandler : IRequestHandler<GetRoomsByHotelIdWithFiltersQuery, List<appDomain.Room>>
{
    private readonly IRoomQueryService _roomQueryService;

    public GetRoomsByHotelIdWithFiltersQueryHandler(IRoomQueryService roomQueryService)
    {
        _roomQueryService = roomQueryService;
    }

    public async Task<List<appDomain.Room>> Handle(GetRoomsByHotelIdWithFiltersQuery request, CancellationToken cancellationToken)
    {
        // Get all rooms for the specific hotel
        List<appDomain.Room> rooms = await _roomQueryService.GetRoomsByHotelIdAsync(request.HotelId, cancellationToken);

        // Filter by available status
       var availableRooms = rooms.Where(r => r.Status == appDomain.RoomStatus.Available);

    // Apply date filters if provided
    if (request.CheckIn.HasValue && request.CheckOut.HasValue)
    {
        DateTime checkIn = request.CheckIn.Value;
        DateTime checkOut = request.CheckOut.Value;

        availableRooms = availableRooms.Where(r => 
            !r.Bookings.Any(b => 
                    b.Status == appDomain.BookingStatus.Confirmed || 
                     b.Status == appDomain.BookingStatus.Pending ||
                b.Status != appDomain.BookingStatus.Cancelled && 
                (checkIn < b.CheckOutDate && 
                checkOut > b.CheckInDate)
            )
        );
    }

    return availableRooms.ToList();
    }
}
