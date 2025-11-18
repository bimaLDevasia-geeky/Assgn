using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command.Employee.Commands
{
    public class UpdateEmployeeCommand : IRequest<domain.Employee>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
