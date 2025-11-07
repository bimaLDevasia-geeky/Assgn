using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.RoomType
{
    public class GetAllRoomTypesQueryHandler : IRequestHandler<GetAllRoomTypesQuery, List<appDomain.RoomType>>
    {
        private readonly HotelBookingDbContext _context;

        public GetAllRoomTypesQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<appDomain.RoomType>> Handle(GetAllRoomTypesQuery request, CancellationToken cancellationToken)
        {
            return await _context.RoomTypes.ToListAsync(cancellationToken);
        }
    }
}
