using Microsoft.EntityFrameworkCore;
using AppDomain = Hotel.Booking.Domain.Entities ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Booking.Infrastructure.Persistance
{
    public class HotelBookingDbContext:DbContext
    {
        public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options):base(options) { 
        
        }
        
        public DbSet<AppDomain.Hotel> Hotels { get; set; }
        public DbSet<AppDomain.Room> Rooms { get; set; }
        public DbSet<AppDomain.RoomType> RoomTypes { get; set; }
        public DbSet<AppDomain.Customer> Customers { get; set; }
        public DbSet<AppDomain.Booking> Bookings { get; set; }
        public DbSet<AppDomain.Payment> Payments { get; set; }
        public DbSet<AppDomain.Employee> Employees { get; set; }
        public DbSet<AppDomain.Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppDomain.Hotel>()
                .Property(h => h.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AppDomain.Room>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AppDomain.RoomType>()
                .Property(rt => rt.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AppDomain.Customer>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AppDomain.Booking>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AppDomain.Payment>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AppDomain.Employee>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AppDomain.Review>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AppDomain.Customer>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AppDomain.Booking>()
                .Property(b => b.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AppDomain.Room>()
                .Property(r => r.PricePerNight)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AppDomain.Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");
            

        }
        
    }
}
