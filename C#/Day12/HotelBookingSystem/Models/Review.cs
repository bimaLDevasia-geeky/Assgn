namespace HotelBookingSystem.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int HostelId {  get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        
        public Hotel Hotel { get; set; }
        public Customer Customer { get; set; }
    }
}
