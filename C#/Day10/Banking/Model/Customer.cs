    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Banking.Model
    {
        public class Customer
        {
            public int Id { set; get; }

            [Required]
            [StringLength(100)]
            public string FullName { set; get; }

            [Required]
            [StringLength(256)]
            public string Email { set; get; }
            public string? PhoneNumber { set; get; }
            public DateTime? DateOfBirth { set; get; }

            public int AddressId { set; get; }

            public Address Address { set; get; }

            public List<Account> accounts { set; get; } = new List<Account>();

            

        }
    }
