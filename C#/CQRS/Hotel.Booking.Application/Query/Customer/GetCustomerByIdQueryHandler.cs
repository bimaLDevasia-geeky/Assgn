using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Customer
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, appDomain.Customer?>
    {
        private readonly HotelBookingDbContext _context;

        public GetCustomerByIdQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<appDomain.Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
