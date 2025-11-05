using Hotel.Booking.Domain.Interfaces;
using MediatR;
using System;

namespace Hotel.Booking.Application.Command
{
    public class DeleteReviewCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, bool>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReviewCommandHandler(IReviewRepository repository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteReviewCommand request, CancellationToken ct)
        {
            var review = await _reviewRepository.GetByIdAsync(request.Id);
            if (review is null)
            {
                throw new KeyNotFoundException("Review not found");
            }

            _reviewRepository.Delete(review);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }
    }
}
