using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;

namespace Hotel.Booking.Application.Query
{
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, List<appDomain.Room>>
    {
        private readonly IRoomRepository _roomRepository;

        public GetAllRoomsQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<appDomain.Room>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _roomRepository.GetAllAsync();
        }
    }
}
