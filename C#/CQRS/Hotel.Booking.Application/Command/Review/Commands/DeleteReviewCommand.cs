using Hotel.Booking.Domain.Interfaces;
using MediatR;
using System;

namespace Hotel.Booking.Application.Command.Review.Commands
{
    public class DeleteReviewCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
