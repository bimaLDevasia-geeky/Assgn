using HotelBookingSystem.Context;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class BookingServices : IBooking
    {
        private readonly HotelBookingSystemContext _context;

        public BookingServices(HotelBookingSystemContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateBooking(Booking booking)
        {
            // Verify room availability
            var isAvailable = await IsRoomAvailable(booking.RoomId, booking.CheckInTime, booking.CheckOutTime);
            if (!isAvailable)
                throw new InvalidOperationException("Room is not available for the selected dates");

            booking.Status = BookingStatus.Pending;
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            // Update room status
            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room != null)
            {
                room.Status = RoomStatus.Booked;
                await _context.SaveChangesAsync();
            }

            return booking;
        }

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingById(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                throw new KeyNotFoundException("Booking not found");

            return booking;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByCustomer(int customerId)
        {
            return await _context.Bookings
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByHotel(int hotelId)
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Where(b => b.Room.HotelId == hotelId)
                .ToListAsync();
        }

        public async Task<Booking> UpdateBooking(Booking booking)
        {
            var existingBooking = await _context.Bookings.FindAsync(booking.Id);
            if (existingBooking == null)
                throw new KeyNotFoundException("Booking not found");

            existingBooking.CheckInTime = booking.CheckInTime;
            existingBooking.CheckOutTime = booking.CheckOutTime;
            existingBooking.Status = booking.Status;
            existingBooking.TotalAmount = booking.TotalAmount;

            await _context.SaveChangesAsync();
            return existingBooking;
        }

        public async Task<Booking> UpdateBookingStatus(int id, BookingStatus status)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                throw new KeyNotFoundException("Booking not found");

            booking.Status = status;

            // Update room status based on booking status
            if (status == BookingStatus.Cancelled)
            {
                booking.Room.Status = RoomStatus.Available;
            }
            else if (status == BookingStatus.Confirmed)
            {
                booking.Room.Status = RoomStatus.Booked;
            }

            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task CancelBooking(int id)
        {
            await UpdateBookingStatus(id, BookingStatus.Cancelled);
        }

        private async Task<bool> IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = await _context.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
                throw new KeyNotFoundException("Room not found");

            if (room.Status != RoomStatus.Available)
                return false;

            return !room.Bookings.Any(b =>
                (checkIn <= b.CheckOutTime && checkOut >= b.CheckInTime) &&
                b.Status != BookingStatus.Cancelled);
        }
    }
}