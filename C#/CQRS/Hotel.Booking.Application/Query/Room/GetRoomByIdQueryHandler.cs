using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Room
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, appDomain.Room?>
    {
        private readonly HotelBookingDbContext _context;

        public GetRoomByIdQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<appDomain.Room?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Rooms.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
