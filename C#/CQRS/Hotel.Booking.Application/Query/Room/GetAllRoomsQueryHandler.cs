using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Room
{
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, List<appDomain.Room>>
    {
        private readonly IRoomQueryService _service;

        public GetAllRoomsQueryHandler(IRoomQueryService service)
        {
        _service = service;
        }

        public async Task<List<appDomain.Room>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllRoomsAsync(cancellationToken);
        }
    }
}
