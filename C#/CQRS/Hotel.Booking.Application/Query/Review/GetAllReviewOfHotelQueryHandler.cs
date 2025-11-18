using System;
using Hotel.Booking.Application.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
namespace Hotel.Booking.Application.Query.Review;

public class GetAllReviewOfHotelQueryHandler(IReviewQueryService service) : IRequestHandler<GetAllReviewOfHotelQuery, List<appDomain.Review>>
{
    public async Task<List<appDomain.Review>> Handle(GetAllReviewOfHotelQuery request, CancellationToken cancellationToken)
    {
        return await service.GetAllReviewsOfHotelAsync(request.HotelId, cancellationToken);
    }
}
