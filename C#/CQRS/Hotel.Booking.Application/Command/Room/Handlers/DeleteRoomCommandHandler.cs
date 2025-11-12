using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Room.Commands;

namespace Hotel.Booking.Application.Command.Room.Handlers
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, bool>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomCommandHandler(IRoomRepository repository, IUnitOfWork unitOfWork)
        {
            _roomRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken ct)
        {
            var room = await _roomRepository.GetByIdAsync(request.Id, ct);
            if (room is null)
            {
                throw new KeyNotFoundException("Room not found");
            }

            _roomRepository.Delete(room);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }
    }
}

