using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Booking.Application.DTOs
{

    public record HotelUpdateDto(
        string Name,
        string Address,
        string City,
        string Country,
        string PhoneNumber
        );
   
}
