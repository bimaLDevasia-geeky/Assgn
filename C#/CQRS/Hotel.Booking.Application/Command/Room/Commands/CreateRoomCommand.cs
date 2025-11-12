using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command.Room.Commands
{
    public class CreateRoomCommand : IRequest<Guid>
    {
        public string RoomNumber { get; set; } = null!;
        public Guid HotelId { get; set; }
        public Guid RoomTypeId { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
