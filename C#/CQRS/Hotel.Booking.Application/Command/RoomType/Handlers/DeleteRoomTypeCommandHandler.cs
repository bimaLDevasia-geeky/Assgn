using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.RoomType.Commands;

namespace Hotel.Booking.Application.Command.RoomType.Handlers
{
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, bool>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomTypeCommandHandler(IRoomTypeRepository repository, IUnitOfWork unitOfWork)
        {
            _roomTypeRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteRoomTypeCommand request, CancellationToken ct)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(request.Id, ct);
            if (roomType is null)
            {
                throw new KeyNotFoundException("RoomType not found");
            }

            _roomTypeRepository.Delete(roomType);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }
    }
}

