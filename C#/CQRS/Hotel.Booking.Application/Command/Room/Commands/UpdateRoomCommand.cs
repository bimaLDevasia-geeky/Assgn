using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command.Room.Commands
{
    public class UpdateRoomCommand : IRequest<domain.Room>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public Guid HotelId { get; set; }
        public Guid RoomTypeId { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
