using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.DTO
{
    public class RoomTypeCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string TypeName { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(1, 10)]
        public int Capacity { get; set; }
    }

    public class RoomTypeResponseDTO
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
    }

    public class RoomTypeUpdateDTO
    {
        [Required]
        [StringLength(50)]
        public string TypeName { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(1, 10)]
        public int Capacity { get; set; }
    }
}
