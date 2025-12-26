using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Model
{
    public class Address
    {
        public int Id { set; get; }

        [Required]
        [StringLength(100)]
        public string Street { set; get; }

        [Required]
        [StringLength(100)] 
        public string City { set; get; }

        [Required]
        [StringLength(100)]
        public string State { set; get; }

        [Required]
        [MaxLength(6)]
        public int PostalCode { set; get; }

        [Required]
        [StringLength(100)]
        public string Country { set; get; }

        public List<Customer> customers { set; get; }

    }
}
