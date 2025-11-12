using Hotel.Booking.Domain.Interfaces;
using MediatR;
using System;

namespace Hotel.Booking.Application.Command.Room.Commands
{
    public class DeleteRoomCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
