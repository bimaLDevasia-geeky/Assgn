using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Review
{
    public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, List<appDomain.Review>>
    {
        private readonly IReviewQueryService _service;

        public GetAllReviewsQueryHandler(IReviewQueryService service)
        {
        _service = service;
        }

        public async Task<List<appDomain.Review>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllReviewsAsync(cancellationToken);
        }
    }
}
