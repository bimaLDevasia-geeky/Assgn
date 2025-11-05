using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;

namespace Hotel.Booking.Application.Query
{
    public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, List<appDomain.Booking>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetAllBookingsQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<appDomain.Booking>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            return await _bookingRepository.GetAllAsync();
        }
    }
}
