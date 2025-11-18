using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Room.Commands;

namespace Hotel.Booking.Application.Command.Room.Handlers
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Guid>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomCommandHandler(IRoomRepository repository, IUnitOfWork unitOfWork)
        {
            _roomRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken ct)
        {
            domain.Room room = domain.Room.Create(
                request.RoomNumber,
                request.HotelId,
                request.RoomTypeId,
                request.PricePerNight,
                request.Status
            );
            
            await _roomRepository.AddAsync(room);
            await _unitOfWork.SaveChangesAsync(ct);
            return room.Id;
        }
    }
}

