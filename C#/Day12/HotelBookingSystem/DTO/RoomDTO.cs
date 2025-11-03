using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.DTO
{
    public class RoomCreateDTO
    {
        [Required]
        [StringLength(10)]
        public string RoomNumber { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerNight { get; set; }

        [Required]
        public int RoomTypeId { get; set; }

        [Required]
        public int HotelId { get; set; }
    }

    public class RoomResponseDTO
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public string Status { get; set; } = null!;
        public decimal PricePerNight { get; set; }
        public RoomTypeResponseDTO RoomType { get; set; } = null!;
        public HotelResponseDTO Hotel { get; set; } = null!;
    }

    public class RoomUpdateDTO
    {
        [Required]
        [StringLength(10)]
        public string RoomNumber { get; set; } = null!;

        [Required]
        public string Status { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerNight { get; set; }

        [Required]
        public int RoomTypeId { get; set; }
    }
}