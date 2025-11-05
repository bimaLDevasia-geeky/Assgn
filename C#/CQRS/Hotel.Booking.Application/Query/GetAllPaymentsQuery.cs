using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query
{
    public class GetAllPaymentsQuery : IRequest<List<appDomain.Payment>>
    {
    }
}
