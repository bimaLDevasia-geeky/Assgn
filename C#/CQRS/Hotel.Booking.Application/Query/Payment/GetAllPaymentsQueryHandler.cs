using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Payment
{
    public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, List<appDomain.Payment>>
    {
        private readonly IPaymentQueryService _service;

        public GetAllPaymentsQueryHandler(IPaymentQueryService service)
        {
        _service = service;
        }

        public async Task<List<appDomain.Payment>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllPaymentsAsync(cancellationToken);
        }
    }
}
