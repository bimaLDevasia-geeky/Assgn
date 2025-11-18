using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Review
{
    public class GetAllReviewsQuery : IRequest<List<appDomain.Review>>
    {
    }
}
