using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Review
{
    public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, List<appDomain.Review>>
    {
        private readonly HotelBookingDbContext _context;

        public GetAllReviewsQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<appDomain.Review>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Reviews.ToListAsync(cancellationToken);
        }
    }
}
