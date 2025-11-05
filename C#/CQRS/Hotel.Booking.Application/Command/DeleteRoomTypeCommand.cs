using Hotel.Booking.Domain.Interfaces;
using MediatR;
using System;

namespace Hotel.Booking.Application.Command
{
    public class DeleteRoomTypeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

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
            var roomType = await _roomTypeRepository.GetByIdAsync(request.Id);
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
