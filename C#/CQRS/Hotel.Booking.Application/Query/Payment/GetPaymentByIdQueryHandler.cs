using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Payment
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, appDomain.Payment?>
    {
        private readonly IPaymentQueryService _service;

        public GetPaymentByIdQueryHandler(IPaymentQueryService service)
        {
        _service = service;
        }

        public async Task<appDomain.Payment?> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaymentByIdAsync(request.Id, cancellationToken);
        }
    }
}
