using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel.Booking.Application.Command.RoomType.Commands
{
    public class UpdateRoomTypeCommand : IRequest<domain.RoomType>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string TypeName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
    }
}
