using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

using Hotel.Booking.Domain.Entities;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
namespace Hotel.Booking.Application.Query;

public class GetAllHotelQueryHandler(HotelBookingDbContext context) : IRequestHandler<GetAllHotelQuery, List<appDomain.Hotel>>
{
	public async Task<List<appDomain.Hotel>> Handle(GetAllHotelQuery request, CancellationToken cancellationToken)
	{
		return await context.Hotels
			.Include(h => h.Rooms)
			.Include(h => h.Employees)
			.ToListAsync(cancellationToken);
	}
}
