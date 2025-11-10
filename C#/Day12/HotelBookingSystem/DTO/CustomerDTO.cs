using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.DTO
{
    public class CustomerCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string IdProofNumber { get; set; } = null!;
    }

    public class CustomerResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string IdProofNumber { get; set; } = null!;
    }

    public class CustomerUpdateDTO
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string IdProofNumber { get; set; } = null!;
    }
}