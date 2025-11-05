using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;

namespace Hotel.Booking.Application.Query
{
    public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, List<appDomain.Review>>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetAllReviewsQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<appDomain.Review>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            return await _reviewRepository.GetAllAsync();
        }
    }
}
