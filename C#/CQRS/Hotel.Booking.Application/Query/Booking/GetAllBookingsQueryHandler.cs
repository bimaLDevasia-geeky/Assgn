using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;
namespace Hotel.Booking.Application.Query.Booking
{
    public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, List<appDomain.Booking>>
    {
        private readonly IBookingQueryService _service;

        public GetAllBookingsQueryHandler(IBookingQueryService service)
        {
        _service = service;
        }

        public async Task<List<appDomain.Booking>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllBookingsAsync(cancellationToken);
        }
    }
}
