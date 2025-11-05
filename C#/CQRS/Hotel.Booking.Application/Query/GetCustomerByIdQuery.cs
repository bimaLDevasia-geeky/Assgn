using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query
{
    public class GetCustomerByIdQuery : IRequest<appDomain.Customer?>
    {
        public Guid Id { get; set; }
    }
}
