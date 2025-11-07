using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Payment
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, appDomain.Payment?>
    {
        private readonly HotelBookingDbContext _context;

        public GetPaymentByIdQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<appDomain.Payment?> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        }
    }
}
