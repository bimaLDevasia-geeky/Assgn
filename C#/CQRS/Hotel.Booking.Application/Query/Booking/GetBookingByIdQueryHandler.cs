using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Booking
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, appDomain.Booking?>
    {
        private readonly IBookingQueryService _service;

        public GetBookingByIdQueryHandler(IBookingQueryService service)
        {
        _service = service;
        }

        public async Task<appDomain.Booking?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetBookingByIdAsync(request.Id, cancellationToken);
        }
    }
}
