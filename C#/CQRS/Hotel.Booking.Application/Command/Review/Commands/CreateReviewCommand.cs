using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

namespace Hotel.Booking.Application.Command.Review.Commands
{
    public class CreateReviewCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
        public Guid HotelId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
    }
}
