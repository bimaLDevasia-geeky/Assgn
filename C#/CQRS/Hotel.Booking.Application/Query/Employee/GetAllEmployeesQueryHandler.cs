using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Employee
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<appDomain.Employee>>
    {
        private readonly HotelBookingDbContext _context;

        public GetAllEmployeesQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<appDomain.Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees.ToListAsync(cancellationToken);
        }
    }
}
