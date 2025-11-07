using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Room
{
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, List<appDomain.Room>>
    {
        private readonly HotelBookingDbContext _context;

        public GetAllRoomsQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<appDomain.Room>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Rooms.ToListAsync(cancellationToken);
        }
    }
}
