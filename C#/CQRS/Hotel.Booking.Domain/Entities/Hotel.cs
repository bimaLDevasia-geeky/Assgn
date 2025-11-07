using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Booking.Domain.Entities
{
    public class Hotel
    {
        

        public Guid Id { get;  private set; }
        public string Name { get; private set; } = null!;
        public string Address { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string Country { get; private set; } = null!;
        public string PhoneNumber { get; private set; } = null!;

        public ICollection<Room> Rooms { get; private set; } = new List<Room>();
        public ICollection<Employee> Employees { get; private set; } = new List<Employee>();
        public static Hotel Create(string name, string address, string city, string phone, string country)
        {

            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Hotel Name is required");

            return new Hotel
            {
                Name = name,
                Address = address,
                City = city,
                Country = country,
                PhoneNumber = phone
            };
        }

        public  void UpdateDetails(string name, string address, string city, string country, string phone)
        {
            Name=name;
            Address=address;
            City=city;
            Country=country;
            PhoneNumber=phone;

        }

    }
}
