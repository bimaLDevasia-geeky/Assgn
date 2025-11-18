using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

namespace Hotel.Booking.Application.Command.Customer.Commands
{
    public class CreateCustomerCommand : IRequest<Guid>
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string IdProofNumber { get; set; } = null!;
    }
}
