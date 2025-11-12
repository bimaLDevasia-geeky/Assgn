using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command.Review.Commands
{
    public class UpdateReviewCommand : IRequest<domain.Review>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
    }
}
