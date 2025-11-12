using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Application.Interfaces;

namespace Hotel.Booking.Application.Query.Hotel;

public class GetAllHotelQueryHandler(IHotelQueryService service) : IRequestHandler<GetAllHotelQuery, List<appDomain.Hotel>>
{
	public async Task<List<appDomain.Hotel>> Handle(GetAllHotelQuery request, CancellationToken cancellationToken)
	{
		return await service.GetAllHotelsAsync(cancellationToken);
	}
}
