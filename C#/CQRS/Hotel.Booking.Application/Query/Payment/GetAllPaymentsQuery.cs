using System;
using System.Collections.Generic;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Payment
{
    public class GetAllPaymentsQuery : IRequest<List<appDomain.Payment>>
    {
    }
}
