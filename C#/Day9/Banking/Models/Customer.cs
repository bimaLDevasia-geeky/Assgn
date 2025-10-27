using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? FullName { get; set; }

        [Required]
        [StringLength(50)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
