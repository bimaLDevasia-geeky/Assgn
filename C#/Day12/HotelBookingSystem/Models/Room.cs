namespace HotelBookingSystem.Models
{


    public enum RoomStatus{
        Available,
        Booked,
        UnderMaintaenance
    }

  

    public class Room
    {
        public int Id { get; set; }

        public string RoomNumber { get; set; } = null!;

        public RoomStatus Status { get; set; }
        public decimal PricePerNight { get; set; }
        public int RoomTypeId { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; } = null!;

        public RoomType RoomType { get; set; } = null!;

        public List<Booking> Bookings { get; set; } = new List<Booking>();

    }   
}
