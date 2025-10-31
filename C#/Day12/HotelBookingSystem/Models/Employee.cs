using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace HotelBookingSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;

        public Hotel Hotel { get; set; }

    }
}
