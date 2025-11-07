using Hotel.Booking.Domain.Interfaces;
using MediatR;
using System;

namespace Hotel.Booking.Application.Command.Room
{
    public class DeleteRoomCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

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
