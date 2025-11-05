using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command
{
    public class UpdatePaymentCommand : IRequest<domain.Payment>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public domain.PaymentStatus? Status { get; set; }
        public decimal? Amount { get; set; }
        public domain.PaymentMethod? PaymentMethod { get; set; }
    }

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
            domain.Payment? payment = await _paymentRepository.GetByIdAsync(request.Id);
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
