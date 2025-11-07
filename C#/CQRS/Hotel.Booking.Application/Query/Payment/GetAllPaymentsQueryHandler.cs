using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Payment
{
    public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, List<appDomain.Payment>>
    {
        private readonly HotelBookingDbContext _context;

        public GetAllPaymentsQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<appDomain.Payment>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Payments.ToListAsync(cancellationToken);
        }
    }
}
