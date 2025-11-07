using System;
using System.Threading;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Review
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, appDomain.Review?>
    {
        private readonly HotelBookingDbContext _context;

        public GetReviewByIdQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<appDomain.Review?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Reviews.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
