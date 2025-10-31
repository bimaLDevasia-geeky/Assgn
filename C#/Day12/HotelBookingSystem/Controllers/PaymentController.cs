using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _services;

        public PaymentController(IPayment services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            try
            {
                var newPayment = await _services.CreatePayment(payment);
                return CreatedAtAction(nameof(GetPaymentById), new { id = newPayment.Id }, newPayment);
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAllPayments()
        {
            try
            {
                var payments = await _services.GetAllPayments();
                return Ok(payments);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPaymentById(int id)
        {
            try
            {
                var payment = await _services.GetPaymentById(id);
                return Ok(payment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Payment not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentsByBooking(int bookingId)
        {
            try
            {
                var payments = await _services.GetPaymentsByBooking(bookingId);
                return Ok(payments);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentsByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var payments = await _services.GetPaymentsByDateRange(startDate, endDate);
                return Ok(payments);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Payment>> UpdatePayment(int id, Payment payment)
        {
            if (id != payment.Id)
                return BadRequest(new { Message = "ID mismatch" });

            try
            {
                var updatedPayment = await _services.UpdatePayment(payment);
                return Ok(updatedPayment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Payment not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<Payment>> UpdatePaymentStatus(int id, [FromBody] PaymentStatus status)
        {
            try
            {
                var updatedPayment = await _services.UpdatePaymentStatus(id, status);
                return Ok(updatedPayment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Payment not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }
}