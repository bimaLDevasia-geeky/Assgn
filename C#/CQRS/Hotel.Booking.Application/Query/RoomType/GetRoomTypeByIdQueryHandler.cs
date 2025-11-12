using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.RoomType
{
    public class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomTypeByIdQuery, appDomain.RoomType?>
    {
        private readonly IRoomTypeQueryService _service;

        public GetRoomTypeByIdQueryHandler(IRoomTypeQueryService service)
        {
        _service = service;
        }

        public async Task<appDomain.RoomType?> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetRoomTypeByIdAsync(request.Id, cancellationToken);
        }
    }
}
