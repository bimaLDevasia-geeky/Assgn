using HotelBookingSystem.DTO;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBooking _services;

        public BookingController(IBooking services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<ActionResult<BookingResponseDTO>> CreateBooking(BookingCreateDTO booking)
        {
            try
            {
                var newBooking = await _services.CreateBooking(booking);
                return CreatedAtAction(nameof(GetBookingById), new { id = newBooking.Id }, newBooking);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Room or Customer not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingResponseDTO>>> GetAllBookings()
        {
            try
            {
                var bookings = await _services.GetAllBookings();
                return Ok(bookings);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            try
            {
                var booking = await _services.GetBookingById(id);
                return Ok(booking);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Booking not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsByCustomer(int customerId)
        {
            try
            {
                var bookings = await _services.GetBookingsByCustomer(customerId);
                return Ok(bookings);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookingResponseDTO>> UpdateBooking(int id, BookingUpdateDTO booking)
        {
            try
            {
                var updatedBooking = await _services.UpdateBooking(id, booking);
                return Ok(updatedBooking);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Booking not found" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<Booking>> UpdateBookingStatus(int id, [FromBody] BookingStatus status)
        {
            try
            {
                var updatedBooking = await _services.UpdateBookingStatus(id, status);
                return Ok(updatedBooking);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Booking not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPost("{id}/cancel")]
        public async Task<ActionResult> CancelBooking(int id)
        {
            try
            {
                await _services.CancelBooking(id);
                return Ok(new { Message = "Booking cancelled successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Booking not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }
}