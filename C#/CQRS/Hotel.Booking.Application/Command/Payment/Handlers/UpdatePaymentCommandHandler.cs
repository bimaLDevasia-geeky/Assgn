using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Payment.Commands;

namespace Hotel.Booking.Application.Command.Payment.Handlers
{
    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, domain.Payment>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePaymentCommandHandler(IPaymentRepository repository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<domain.Payment> Handle(UpdatePaymentCommand request, CancellationToken ct)
        {
            domain.Payment? payment = await _paymentRepository.GetByIdAsync(request.Id, ct);
            if (payment is null)
            {
                throw new KeyNotFoundException("Payment not found");
            }
            
            payment.UpdateDetails(request.Status, request.Amount, request.PaymentMethod);
            await _unitOfWork.SaveChangesAsync(ct);
            return payment;
        }
    }
}

