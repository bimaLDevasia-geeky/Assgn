using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Payment.Commands;

namespace Hotel.Booking.Application.Command.Payment.Handlers
{
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, bool>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePaymentCommandHandler(IPaymentRepository repository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken ct)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.Id, ct);
            if (payment is null)
            {
                throw new KeyNotFoundException("Payment not found");
            }

            _paymentRepository.Delete(payment);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }
    }
}

