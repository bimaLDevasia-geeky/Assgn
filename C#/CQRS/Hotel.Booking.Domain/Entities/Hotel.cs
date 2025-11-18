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
        public int? StarRating { get; private set; } // 1 to 5 stars (nullable for existing data)

        public ICollection<Room> Rooms { get; private set; } = new List<Room>();
        public ICollection<Employee> Employees { get; private set; } = new List<Employee>();

        public ICollection<Review> Reviews { get; private set; } = new List<Review>();
        public static Hotel Create(string name, string address, string city, string phone, string country, int? starRating = 3)
        {

            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Hotel Name is required");
            if (starRating.HasValue && (starRating.Value < 1 || starRating.Value > 5)) 
                throw new ArgumentException("Star rating must be between 1 and 5");

            return new Hotel
            {
                Name = name,
                Address = address,
                City = city,
                Country = country,
                PhoneNumber = phone,
                StarRating = starRating
            };
        }

        public  void UpdateDetails(string name, string address, string city, string country, string phone, int? starRating)
        {
            if (starRating.HasValue && (starRating.Value < 1 || starRating.Value > 5)) 
                throw new ArgumentException("Star rating must be between 1 and 5");
            
            Name=name;
            Address=address;
            City=city;
            Country=country;
            PhoneNumber=phone;
            StarRating=starRating;

        }

    }
}
