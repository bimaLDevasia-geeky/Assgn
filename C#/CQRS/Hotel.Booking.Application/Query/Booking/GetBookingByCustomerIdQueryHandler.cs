using System;
using Hotel.Booking.Application.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Booking;

public class GetBookingByCustomerIdQueryHandler:IRequestHandler<GetBookingByCustomerIdQuery, List<appDomain.Booking>>
{
    private readonly IBookingQueryService _service;
    public GetBookingByCustomerIdQueryHandler(IBookingQueryService service)
    {
        _service = service;
    }
    public async Task<List<appDomain.Booking>> Handle(GetBookingByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetBookingsByCustomerIdAsync(request.CustomerId, cancellationToken);
    }
}
