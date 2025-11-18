using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Room
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, appDomain.Room?>
    {
        private readonly IRoomQueryService _service;

        public GetRoomByIdQueryHandler(IRoomQueryService service)
        {
        _service = service;
        }

        public async Task<appDomain.Room?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetRoomByIdAsync(request.Id, cancellationToken);
        }
    }
}
