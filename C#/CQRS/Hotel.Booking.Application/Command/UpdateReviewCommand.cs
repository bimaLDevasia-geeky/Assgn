using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command
{
    public class UpdateReviewCommand : IRequest<domain.Review>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
    }

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
            domain.Review? review = await _reviewRepository.GetByIdAsync(request.Id);
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
