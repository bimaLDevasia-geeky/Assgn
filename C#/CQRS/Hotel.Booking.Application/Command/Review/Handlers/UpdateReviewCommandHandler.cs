using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Review.Commands;

namespace Hotel.Booking.Application.Command.Review.Handlers
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, domain.Review>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReviewCommandHandler(IReviewRepository repository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<domain.Review> Handle(UpdateReviewCommand request, CancellationToken ct)
        {
            domain.Review? review = await _reviewRepository.GetByIdAsync(request.Id, ct);
            if (review is null)
            {
                throw new KeyNotFoundException("Review not found");
            }
            
            review.UpdateDetails(request.Rating, request.Comment);
            await _unitOfWork.SaveChangesAsync(ct);
            return review;
        }
    }
}

