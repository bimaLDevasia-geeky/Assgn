using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;

namespace Hotel.Booking.Application.Query
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, appDomain.Payment?>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<appDomain.Payment?> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _paymentRepository.GetByIdAsync(request.Id);
        }
    }
}
