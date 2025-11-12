using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.RoomType
{
    public class GetAllRoomTypesQueryHandler : IRequestHandler<GetAllRoomTypesQuery, List<appDomain.RoomType>>
    {
        private readonly IRoomTypeQueryService _service;

        public GetAllRoomTypesQueryHandler(IRoomTypeQueryService service)
        {
        _service = service;
        }

        public async Task<List<appDomain.RoomType>> Handle(GetAllRoomTypesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllRoomTypesAsync(cancellationToken);
        }
    }
}
