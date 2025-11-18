using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.RoomType.Commands;

namespace Hotel.Booking.Application.Command.RoomType.Handlers
{
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, Guid>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomTypeCommandHandler(IRoomTypeRepository repository, IUnitOfWork unitOfWork)
        {
            _roomTypeRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateRoomTypeCommand request, CancellationToken ct)
        {
            domain.RoomType roomType = domain.RoomType.Create(
                request.TypeName,
                request.Description,
                request.Capacity
            );
            
            await _roomTypeRepository.AddAsync(roomType);
            await _unitOfWork.SaveChangesAsync(ct);
            return roomType.Id;
        }
    }
}

