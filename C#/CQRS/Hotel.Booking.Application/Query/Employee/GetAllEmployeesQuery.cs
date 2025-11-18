using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Employee
{
    public class GetAllEmployeesQuery : IRequest<List<appDomain.Employee>>
    {
    }
}
