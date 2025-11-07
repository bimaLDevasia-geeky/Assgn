using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command.Booking
{
    public class UpdateBookingCommand : IRequest<domain.Booking>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public domain.BookingStatus? Status { get; set; }
        public decimal? TotalAmount { get; set; }
        public Guid? RoomId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }

    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, domain.Booking>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookingCommandHandler(IBookingRepository repository, IUnitOfWork unitOfWork)
        {
            _bookingRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<domain.Booking> Handle(UpdateBookingCommand request, CancellationToken ct)
        {
            domain.Booking? booking = await _bookingRepository.GetByIdAsync(request.Id, ct);
            if (booking is null)
            {
                throw new KeyNotFoundException("Booking not found");
            }
            
            booking.UpdateBookingDetails(request.Status, request.TotalAmount, request.RoomId, request.CheckInDate, request.CheckOutDate);
            await _unitOfWork.SaveChangesAsync(ct);
            return booking;
        }
    }
}
