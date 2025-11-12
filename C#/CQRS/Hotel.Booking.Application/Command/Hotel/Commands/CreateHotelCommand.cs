using Hotel.Booking.Domain.Interfaces;
using MediatR;

using appDomain = Hotel.Booking.Domain.Entities;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Booking.Application.Command.Hotel.Commands
{
    public class CreateHotelCommand:IRequest<Guid>
    {
        
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }

    
}
