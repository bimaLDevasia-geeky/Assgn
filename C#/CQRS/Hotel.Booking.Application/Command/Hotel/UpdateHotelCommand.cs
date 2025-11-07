using Hotel.Booking.Application.DTOs;
using appDomain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Hotel.Booking.Domain.Interfaces;

namespace Hotel.Booking.Application.Command.Hotel
{
    public class UpdateHotelCommand:IRequest<appDomain.Hotel>
    {
        [JsonIgnore]
        public Guid Id { get;  set; } 
        public string Name { get;  set; } = null!;
        public string Address { get;  set; } = null!;
        public string City { get;  set; } = null!;
        public string Country { get;  set; } = null!;
        public string PhoneNumber { get;  set; } = null!;
    }

    
}
