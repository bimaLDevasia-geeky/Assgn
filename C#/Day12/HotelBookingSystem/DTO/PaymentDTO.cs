using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.DTO
{
    public class PaymentCreateDTO
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public string Method { get; set; } = null!;
    }

    public class PaymentResponseDTO
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;
        public string Method { get; set; } = null!;
    }

    public class PaymentUpdateDTO
    {
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public string Status { get; set; } = null!;

        [Required]
        public string Method { get; set; } = null!;
    }
}