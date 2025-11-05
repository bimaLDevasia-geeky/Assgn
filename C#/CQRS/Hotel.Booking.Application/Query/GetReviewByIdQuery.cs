using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query
{
    public class GetReviewByIdQuery : IRequest<appDomain.Review?>
    {
        public Guid Id { get; set; }
    }
}
