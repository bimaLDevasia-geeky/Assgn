using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;
using Hotel.Booking.Application.Command.Booking.Commands;
namespace Hotel.Booking.Application.Command.Booking.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookingCommandHandler(IBookingRepository repository, IUnitOfWork unitOfWork)
        {
            _bookingRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken ct)
        {
            domain.Booking booking = domain.Booking.Create(
                request.CustomerId,
                request.RoomId,
                request.CheckInDate,
                request.CheckOutDate,
                request.TotalAmount
            );
            
            await _bookingRepository.AddAsync(booking);
            await _unitOfWork.SaveChangesAsync(ct);
            return booking.Id;
        }
    }
}

