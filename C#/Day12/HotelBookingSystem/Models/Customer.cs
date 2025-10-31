namespace HotelBookingSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string IdProofNumber { get; set; } = null!;

        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
