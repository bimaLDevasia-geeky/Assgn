using HotelBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Context
{
    public class HotelBookingSystemContext:DbContext
    {
        public HotelBookingSystemContext(DbContextOptions<HotelBookingSystemContext> options):base(options)
        { 
        
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>(Booking =>
            {
                Booking.Property(b => b.TotalAmount)
                .HasPrecision(18, 2);
            });


            modelBuilder.Entity<Payment>(Payment =>
            {
                Payment.Property(p=>p.Amount)
                .HasPrecision(18, 2);
            });


            modelBuilder.Entity<Room>(Room =>
            {
                Room.Property(r => r.PricePerNight)
                .HasPrecision(18, 2);
            });
        }

    }
}
