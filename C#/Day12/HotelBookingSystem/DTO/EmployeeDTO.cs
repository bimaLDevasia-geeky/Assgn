using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.DTO
{
    public class EmployeeCreateDTO
    {
        [Required]
        public int HotelId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;
    }

    public class EmployeeResponseDTO
    {
        public int Id { get; set; }
        public HotelResponseDTO Hotel { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    public class EmployeeUpdateDTO
    {
        [Required]
        public int HotelId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;
    }
}