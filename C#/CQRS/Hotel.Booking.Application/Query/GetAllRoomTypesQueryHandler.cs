using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;

namespace Hotel.Booking.Application.Query
{
    public class GetAllRoomTypesQueryHandler : IRequestHandler<GetAllRoomTypesQuery, List<appDomain.RoomType>>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public GetAllRoomTypesQueryHandler(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task<List<appDomain.RoomType>> Handle(GetAllRoomTypesQuery request, CancellationToken cancellationToken)
        {
            return await _roomTypeRepository.GetAllAsync();
        }
    }
}
