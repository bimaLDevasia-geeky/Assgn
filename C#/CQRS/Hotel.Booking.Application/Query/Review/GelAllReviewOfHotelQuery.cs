using System;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
namespace Hotel.Booking.Application.Query.Review;

public class GetAllReviewOfHotelQuery : IRequest<List<appDomain.Review>>
{
    public Guid HotelId { get; set; }
}
