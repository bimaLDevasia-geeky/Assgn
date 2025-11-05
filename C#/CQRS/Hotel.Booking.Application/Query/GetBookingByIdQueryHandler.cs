using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;

namespace Hotel.Booking.Application.Query
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, appDomain.Booking?>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingByIdQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<appDomain.Booking?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            return await _bookingRepository.GetByIdAsync(request.Id);
        }
    }
}
