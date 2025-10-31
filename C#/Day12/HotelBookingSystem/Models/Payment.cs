namespace HotelBookingSystem.Models
{
    public enum PaymentMethod
    {
        Cash,
        Card,
        UPI,
        Online
    }

    public enum PaymentStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    public class Payment
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public PaymentMethod Method { get; set; }

        public Booking Booking { get; set; } = null!;

    }
}
