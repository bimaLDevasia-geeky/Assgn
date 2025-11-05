using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel.Booking.Application.Command
{
    public class UpdateRoomTypeCommand : IRequest<domain.RoomType>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string TypeName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
    }

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
            domain.RoomType? roomType = await _roomTypeRepository.GetByIdAsync(request.Id);
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
