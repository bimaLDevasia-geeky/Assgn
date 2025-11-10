using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.DTO
{
    public class BookingCreateDTO
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime CheckInTime { get; set; }

        [Required]
        public DateTime CheckOutTime { get; set; }
    }

    public class BookingResponseDTO
    {
        public int Id { get; set; }
        public CustomerResponseDTO Customer { get; set; } = null!;
        public RoomResponseDTO Room { get; set; } = null!;
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public string Status { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public PaymentResponseDTO? Payment { get; set; }
    }

    public class BookingUpdateDTO
    {
        [Required]
        public DateTime CheckInTime { get; set; }

        [Required]
        public DateTime CheckOutTime { get; set; }

        [Required]
        public string Status { get; set; } = null!;
    }
}