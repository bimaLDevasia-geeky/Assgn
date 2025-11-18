using System;
using MediatR;

namespace Hotel.Booking.Application.Query.Review;

public class GetAverageReviewOfOneHotel:IRequest<double>
{
    public Guid HotelId { get; set; }
}
