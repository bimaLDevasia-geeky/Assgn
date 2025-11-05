using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query
{
    public class GetEmployeeByIdQuery : IRequest<appDomain.Employee?>
    {
        public Guid Id { get; set; }
    }
}
