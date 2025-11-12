using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.RoomType.Commands;

namespace Hotel.Booking.Application.Command.RoomType.Handlers
{
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, domain.RoomType>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoomTypeCommandHandler(IRoomTypeRepository repository, IUnitOfWork unitOfWork)
        {
            _roomTypeRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<domain.RoomType> Handle(UpdateRoomTypeCommand request, CancellationToken ct)
        {
            domain.RoomType? roomType = await _roomTypeRepository.GetByIdAsync(request.Id, ct);
            if (roomType is null)
            {
                throw new KeyNotFoundException("RoomType not found");
            }
            
            roomType.UpdateDetails(request.TypeName, request.Description, request.Capacity);
            await _unitOfWork.SaveChangesAsync(ct);
            return roomType;
        }
    }
}

