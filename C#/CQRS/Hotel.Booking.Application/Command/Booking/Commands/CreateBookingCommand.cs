using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;
using Hotel.Booking.Application.DTOs;

namespace Hotel.Booking.Application.Command.Booking.Commands
{
    public class CreateBookingCommand : IRequest<BookingResponseDTO>
    {
        public Guid CustomerId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        
    }
}
