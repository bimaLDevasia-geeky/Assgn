using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;

namespace Hotel.Booking.Application.Query
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<appDomain.Customer>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<appDomain.Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetAllAsync();
        }
    }
}
