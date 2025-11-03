using HotelBookingSystem.Context;
using HotelBookingSystem.DTO;
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

        public async Task<BookingResponseDTO> CreateBooking(BookingCreateDTO bookingDto)
        {
            // Verify room availability
            var isAvailable = await IsRoomAvailable(bookingDto.RoomId, bookingDto.CheckInTime, bookingDto.CheckOutTime);
            if (!isAvailable)
                throw new InvalidOperationException("Room is not available for the selected dates");

            var booking = new Booking
            {
                CustomerId = bookingDto.CustomerId,
                RoomId = bookingDto.RoomId,
                CheckInTime = bookingDto.CheckInTime,
                CheckOutTime = bookingDto.CheckOutTime,
                Status = BookingStatus.Pending
            };

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            // Update room status
            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room is not null)
            {
                room.Status = RoomStatus.Booked;
                await _context.SaveChangesAsync();
            }

           
           

            return await CreateBookingResponseDTO(booking);
        }

        public async Task<IEnumerable<BookingResponseDTO>> GetAllBookings()
        {
            List<Booking> bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Include(b => b.Payment)
                .ToListAsync();

            return await Task.WhenAll(bookings.Select(async b => await CreateBookingResponseDTO(b)));
        }

        private async Task<BookingResponseDTO> CreateBookingResponseDTO(Booking booking)
        {
            return new BookingResponseDTO
            {
                Id = booking.Id,
                CheckInTime = booking.CheckInTime,
                CheckOutTime = booking.CheckOutTime,
                Status = booking.Status.ToString(),
                TotalAmount = booking.TotalAmount,
                Customer = new CustomerResponseDTO
                {
                    Id = booking.Customer.Id,
                    FullName = booking.Customer.FullName,
                    Email = booking.Customer.Email,
                    PhoneNumber = booking.Customer.PhoneNumber,
                    IdProofNumber = booking.Customer.IdProofNumber
                },
                Room = new RoomResponseDTO
                {
                    Id = booking.Room.Id,
                    RoomNumber = booking.Room.RoomNumber,
                    Status = booking.Room.Status.ToString(),
                    PricePerNight = booking.Room.PricePerNight,
                    RoomType = new RoomTypeResponseDTO
                    {
                        Id = booking.Room.RoomType.Id,
                        TypeName = booking.Room.RoomType.TypeName,
                        Description = booking.Room.RoomType.Description,
                        Capacity = booking.Room.RoomType.Capacity
                    },
                    Hotel = new HotelResponseDTO
                    {
                        Id = booking.Room.Hotel.Id,
                        Name = booking.Room.Hotel.Name,
                        Address = booking.Room.Hotel.Address,
                        City = booking.Room.Hotel.City,
                        Country = booking.Room.Hotel.Country,
                        PhoneNumber = booking.Room.Hotel.PhoneNumber
                    }
                },
                Payment = booking.Payment is not null ? new PaymentResponseDTO
                {
                    Id = booking.Payment.Id,
                    BookingId = booking.Payment.BookingId,
                    PaymentDate = booking.Payment.PaymentDate,
                    Amount = booking.Payment.Amount,
                    Status = booking.Payment.Status.ToString(),
                    Method = booking.Payment.Method.ToString()
                } : null
            };
        }

        public async Task<BookingResponseDTO> GetBookingById(int id)
        {
            Booking booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Include(b => b.Payment)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking is null)
                throw new KeyNotFoundException("Booking not found");

            return await CreateBookingResponseDTO(booking);
        }

        public async Task<IEnumerable<BookingResponseDTO>> GetBookingsByCustomer(int customerId)
        {
            List<Booking> bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Include(b => b.Payment)
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();

            return await Task.WhenAll(bookings.Select(async b => await CreateBookingResponseDTO(b)));
        }

        private async Task<IEnumerable<BookingResponseDTO>> GetBookingsByHotel(int hotelId)
        {
            List<Booking> bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Include(b => b.Payment)
                .Where(b => b.Room.HotelId == hotelId)
                .ToListAsync();

            return await Task.WhenAll(bookings.Select(async b => await CreateBookingResponseDTO(b)));
        }

        public async Task<BookingResponseDTO> UpdateBooking(int id, BookingUpdateDTO bookingDto)
        {
            Booking existingBooking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Include(b => b.Payment)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (existingBooking == null)
                throw new KeyNotFoundException("Booking not found");

            // Verify if the new dates are available (only if dates are being changed)
            if (existingBooking.CheckInTime != bookingDto.CheckInTime || 
                existingBooking.CheckOutTime != bookingDto.CheckOutTime)
            {
                bool isAvailable = await IsRoomAvailable(
                    existingBooking.RoomId,
                    bookingDto.CheckInTime,
                    bookingDto.CheckOutTime);

                if (!isAvailable)
                    throw new InvalidOperationException("Room is not available for the selected dates");
            }

            // Calculate nights and total amount
            int nights = (bookingDto.CheckOutTime - bookingDto.CheckInTime).Days;
            if (nights <= 0)
                throw new InvalidOperationException("Check-out time must be after check-in time");

            existingBooking.CheckInTime = bookingDto.CheckInTime;
            existingBooking.CheckOutTime = bookingDto.CheckOutTime;
            existingBooking.Status = Enum.Parse<BookingStatus>(bookingDto.Status);
            existingBooking.TotalAmount = existingBooking.Room.PricePerNight * nights;

            // Update room status based on booking status
            if (existingBooking.Status == BookingStatus.Cancelled)
            {
                existingBooking.Room.Status = RoomStatus.Available;
            }
            else if (existingBooking.Status == BookingStatus.Confirmed)
            {
                existingBooking.Room.Status = RoomStatus.Booked;
            }

            await _context.SaveChangesAsync();
            return await CreateBookingResponseDTO(existingBooking);
        }

        public async Task<BookingResponseDTO> UpdateBookingStatus(int id, BookingStatus status)
        {
            Booking booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Include(b => b.Payment)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking is null)
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
            return await CreateBookingResponseDTO(booking);
        }

        public async Task CancelBooking(int id)
        {
            await UpdateBookingStatus(id, BookingStatus.Cancelled);
        }

        private async Task<bool> IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            Room room = await _context.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room is null)
                throw new KeyNotFoundException("Room not found");

            if (room.Status != RoomStatus.Available)
                return false;

            return !room.Bookings.Any(b =>
                (checkIn <= b.CheckOutTime && checkOut >= b.CheckInTime) &&
                b.Status != BookingStatus.Cancelled);
        }
    }
}