using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

namespace Hotel.Booking.Application.Command.Booking.Commands
{
    public class CreateBookingCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
