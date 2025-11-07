using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Customer
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<appDomain.Customer>>
    {
        private readonly HotelBookingDbContext _context;

        public GetAllCustomersQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<appDomain.Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers.ToListAsync(cancellationToken);
        }
    }
}
