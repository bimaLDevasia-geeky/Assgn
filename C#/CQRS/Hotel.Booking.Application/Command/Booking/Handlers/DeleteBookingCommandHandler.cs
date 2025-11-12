using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Booking.Commands;

namespace Hotel.Booking.Application.Command.Booking.Handlers
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookingCommandHandler(IBookingRepository repository, IUnitOfWork unitOfWork)
        {
            _bookingRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken ct)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.Id, ct);
            if (booking is null)
            {
                throw new KeyNotFoundException("Booking not found");
            }

            _bookingRepository.Delete(booking);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }
    }
}

