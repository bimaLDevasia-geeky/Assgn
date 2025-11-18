using System;
using System.Threading;

using MediatR;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Review
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, appDomain.Review?>
    {
        private readonly IReviewQueryService _service;

        public GetReviewByIdQueryHandler(IReviewQueryService service)
        {
        _service = service;
        }

        public async Task<appDomain.Review?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetReviewByIdAsync(request.Id, cancellationToken);
        }
    }
}
