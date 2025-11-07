using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Booking
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, appDomain.Booking?>
    {
        private readonly HotelBookingDbContext _context;

        public GetBookingByIdQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<appDomain.Booking?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Bookings.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
