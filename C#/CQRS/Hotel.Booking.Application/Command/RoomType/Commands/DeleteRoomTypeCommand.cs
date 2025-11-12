using Hotel.Booking.Domain.Interfaces;
using MediatR;
using System;

namespace Hotel.Booking.Application.Command.RoomType.Commands
{
    public class DeleteRoomTypeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
