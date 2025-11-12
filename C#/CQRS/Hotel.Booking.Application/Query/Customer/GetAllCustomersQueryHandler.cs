using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Customer
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<appDomain.Customer>>
    {
        private readonly ICustomerQueryService _service;

        public GetAllCustomersQueryHandler(ICustomerQueryService service)
        {
        _service = service;
        }

        public async Task<List<appDomain.Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllCustomersAsync(cancellationToken);
        }
    }
}
