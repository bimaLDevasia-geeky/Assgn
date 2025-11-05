using System;
using System.Threading;

using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;

namespace Hotel.Booking.Application.Query
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, appDomain.Review?>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewByIdQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<appDomain.Review?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            return await _reviewRepository.GetByIdAsync(request.Id);
        }
    }
}
