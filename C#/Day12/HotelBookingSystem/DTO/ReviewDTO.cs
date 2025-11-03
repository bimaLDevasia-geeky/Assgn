using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.DTO
{
    public class ReviewCreateDTO
    {
        [Required]
        public int HostelId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = null!;
    }

    public class ReviewResponseDTO
    {
        public int Id { get; set; }
        public HotelResponseDTO Hotel { get; set; } = null!;
        public CustomerResponseDTO Customer { get; set; } = null!;
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime ReviewDate { get; set; }
    }

    public class ReviewUpdateDTO
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = null!;
    }
}