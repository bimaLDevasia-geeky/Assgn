using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command.Room
{
    public class CreateRoomCommand : IRequest<Guid>
    {
        public string RoomNumber { get; set; } = null!;
        public Guid HotelId { get; set; }
        public Guid RoomTypeId { get; set; }
        public decimal PricePerNight { get; set; }
    }

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
                request.PricePerNight
            );
            
            await _roomRepository.AddAsync(room);
            await _unitOfWork.SaveChangesAsync(ct);
            return room.Id;
        }
    }
}
