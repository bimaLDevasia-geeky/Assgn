using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Customer
{
    public class GetAllCustomersQuery : IRequest<List<appDomain.Customer>>
    {
    }
}
