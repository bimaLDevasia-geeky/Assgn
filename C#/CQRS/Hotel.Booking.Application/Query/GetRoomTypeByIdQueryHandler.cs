using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;

namespace Hotel.Booking.Application.Query
{
    public class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomTypeByIdQuery, appDomain.RoomType?>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public GetRoomTypeByIdQueryHandler(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task<appDomain.RoomType?> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _roomTypeRepository.GetByIdAsync(request.Id);
        }
    }
}
