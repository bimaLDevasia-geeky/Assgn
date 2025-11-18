using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
namespace Hotel.Booking.Application.Query.Customer;

public class GetCustomerByEmail:IRequest<appDomain.Customer>
{
    public string Email { get; set; } = null!;
}
