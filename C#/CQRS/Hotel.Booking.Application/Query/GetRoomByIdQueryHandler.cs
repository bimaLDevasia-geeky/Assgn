using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;

namespace Hotel.Booking.Application.Query
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, appDomain.Room?>
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomByIdQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<appDomain.Room?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            return await _roomRepository.GetByIdAsync(request.Id);
        }
    }
}
