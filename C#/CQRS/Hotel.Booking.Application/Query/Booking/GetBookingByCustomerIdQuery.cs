using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
namespace Hotel.Booking.Application.Query.Booking;

public class GetBookingByCustomerIdQuery:IRequest<List<appDomain.Booking>>
{
    public  Guid CustomerId { get; set; }
}
