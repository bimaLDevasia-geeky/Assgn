using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Room.Commands;

namespace Hotel.Booking.Application.Command.Room.Handlers
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, domain.Room>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoomCommandHandler(IRoomRepository repository, IUnitOfWork unitOfWork)
        {
            _roomRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<domain.Room> Handle(UpdateRoomCommand request, CancellationToken ct)
        {
            domain.Room? room = await _roomRepository.GetByIdAsync(request.Id, ct);
            if (room is null)
            {
                throw new KeyNotFoundException("Room not found");
            }
            
            room.UpdateDetails(request.RoomNumber, request.RoomTypeId, request.HotelId, request.PricePerNight, request.Status);
            await _unitOfWork.SaveChangesAsync(ct);
            return room;
        }
    }
}

