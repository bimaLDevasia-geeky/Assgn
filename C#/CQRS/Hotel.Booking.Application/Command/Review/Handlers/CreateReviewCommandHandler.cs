using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Review.Commands;

namespace Hotel.Booking.Application.Command.Review.Handlers
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReviewCommandHandler(IReviewRepository repository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken ct)
        {
            domain.Review review = domain.Review.Create(
                request.CustomerId,
                request.HotelId,
                request.Rating,
                request.Comment
            );
            
            await _reviewRepository.AddAsync(review);
            await _unitOfWork.SaveChangesAsync(ct);
            return review.Id;
        }
    }
}

