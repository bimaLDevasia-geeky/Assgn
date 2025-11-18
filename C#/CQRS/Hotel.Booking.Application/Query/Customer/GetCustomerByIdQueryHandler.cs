using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Customer
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, appDomain.Customer?>
    {
        private readonly ICustomerQueryService _service;

        public GetCustomerByIdQueryHandler(ICustomerQueryService service)
        {
        _service = service;
        }

        public async Task<appDomain.Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCustomerByIdAsync(request.Id, cancellationToken);
        }
    }
}
