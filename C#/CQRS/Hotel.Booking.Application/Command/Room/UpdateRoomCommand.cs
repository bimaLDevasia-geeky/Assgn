using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command.Room
{
    public class UpdateRoomCommand : IRequest<domain.Room>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public Guid HotelId { get; set; }
        public Guid RoomTypeId { get; set; }
        public decimal PricePerNight { get; set; }
    }

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
            
            room.UpdateDetails(request.RoomNumber, request.RoomTypeId, request.HotelId, request.PricePerNight);
            await _unitOfWork.SaveChangesAsync(ct);
            return room;
        }
    }
}
