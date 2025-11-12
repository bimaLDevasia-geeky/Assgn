using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Employee
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, appDomain.Employee?>
    {
        private readonly IEmployeeQueryService _service;

        public GetEmployeeByIdQueryHandler(IEmployeeQueryService service)
        {
        _service = service;
        }

        public async Task<appDomain.Employee?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetEmployeeByIdAsync(request.Id, cancellationToken);
        }
    }
}
