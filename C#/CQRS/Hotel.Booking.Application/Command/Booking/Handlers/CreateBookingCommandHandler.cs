using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;
using Hotel.Booking.Application.Command.Booking.Commands;
using Hotel.Booking.Application.Interfaces;
using Hotel.Booking.Application.DTOs;
namespace Hotel.Booking.Application.Command.Booking.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingResponseDTO>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IPaymentService _paymentService;

        public CreateBookingCommandHandler(IBookingRepository repository, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _bookingRepository = repository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }

        public async Task<BookingResponseDTO> Handle(CreateBookingCommand request, CancellationToken ct)
        {   
            
            string razorpayOrderId = await _paymentService.CreateOrderAsync(request.TotalAmount);
            if (string.IsNullOrEmpty(razorpayOrderId))
            {
                throw new Exception("Failed to create payment order");
            }
            
            domain.Booking booking = domain.Booking.Create(
                request.CustomerId,
                request.RoomId,
                request.CheckInDate,
                request.CheckOutDate,
                request.TotalAmount,
                razorpayOrderId
            );
            
            await _bookingRepository.AddAsync(booking);
            await _unitOfWork.SaveChangesAsync(ct);
            return new BookingResponseDTO
            {
                BookingId = booking.Id,
                RazorpayOrderId = razorpayOrderId,
                Amount = booking.TotalAmount,
                Key = "rzp_test_RgpG77jHx7pYHv" 
            };
        }
    }
}

