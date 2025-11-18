using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command.Booking.Commands
{
    public class UpdateBookingCommand : IRequest<domain.Booking>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public domain.BookingStatus? Status { get; set; }
        public decimal? TotalAmount { get; set; }
        public Guid? RoomId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
}
