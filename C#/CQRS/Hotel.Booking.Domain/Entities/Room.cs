using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Booking.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; private set; }
        public string RoomNumber { get; private set; } = null!;
        public Guid HotelId { get; private set; }
        public RoomStatus Status { get; private set; }
        public Guid RoomTypeId { get; private set; }
        public decimal PricePerNight { get; private set; }

        public Hotel Hotel { get; private set; } = null!;
        public RoomType RoomType { get; private set; } = null!;
        public ICollection<Booking> Bookings { get; private set; } = new List<Booking>();

        public static Room Create(string roomNumber, Guid hotelId, Guid roomTypeId, decimal pricePerNight)
        {
            return new Room
            {
                RoomNumber = roomNumber,
                HotelId = hotelId,
                RoomTypeId = roomTypeId,
                PricePerNight = pricePerNight,
                Status = RoomStatus.Available
            };
        }
    

        public void UpdateDetails(string? roomNumber, Guid? roomTypeId, Guid? hotelId, decimal? pricePerNight)
        {
            if (!string.IsNullOrWhiteSpace(roomNumber))
            {
                RoomNumber = roomNumber;
            }
            if (pricePerNight.HasValue && pricePerNight > 0)
            {
                PricePerNight = pricePerNight.Value;
            }
            if (roomTypeId.HasValue && roomTypeId != Guid.Empty)
            {
                RoomTypeId = roomTypeId.Value;
            }
            if (hotelId.HasValue && hotelId != Guid.Empty)
            {
                HotelId = hotelId.Value;
            }

        }
    }

    public enum RoomStatus
    {
        Available,
        Occupied,
        Maintenance
    }
    
    
}
