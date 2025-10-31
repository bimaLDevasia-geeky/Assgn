namespace HotelBookingSystem.Models
{

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }
    public class Booking
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }

        public BookingStatus Status { get; set;}

        public decimal TotalAmount {  get; set; }
        
        public Customer Customer { get; set; }

        public Room Room { get; set; }

        public Payment Payment { get; set; }

    }
}
