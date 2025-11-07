using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;
namespace Hotel.Booking.Application.Query.Booking
{
    public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, List<appDomain.Booking>>
    {
        private readonly HotelBookingDbContext _context;

        public GetAllBookingsQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<appDomain.Booking>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Bookings.ToListAsync(cancellationToken);
        }
    }
}
