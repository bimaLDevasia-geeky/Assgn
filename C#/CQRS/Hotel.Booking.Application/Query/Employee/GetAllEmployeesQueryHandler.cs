using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Employee
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<appDomain.Employee>>
    {
        private readonly IEmployeeQueryService _service;

        public GetAllEmployeesQueryHandler(IEmployeeQueryService service)
        {
        _service = service;
        }

        public async Task<List<appDomain.Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllEmployeesAsync(cancellationToken);
        }
    }
}
