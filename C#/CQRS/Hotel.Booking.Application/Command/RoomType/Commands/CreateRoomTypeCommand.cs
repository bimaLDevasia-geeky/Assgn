using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel.Booking.Application.Command.RoomType.Commands
{
    public class CreateRoomTypeCommand : IRequest<Guid>
    {
        public string TypeName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
    }
}
