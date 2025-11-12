using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Payment.Commands;

namespace Hotel.Booking.Application.Command.Payment.Handlers
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Guid>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePaymentCommandHandler(IPaymentRepository repository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken ct)
        {
            domain.Payment payment = domain.Payment.Create(
                request.BookingId,
                request.Amount,
                request.PaymentMethod
            );
            
            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync(ct);
            return payment.Id;
        }
    }
}

