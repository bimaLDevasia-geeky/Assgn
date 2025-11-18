
using Hotel.Booking.Application.Query.Booking;
using MediatR;
using Hotel.Booking.Application.Command.Booking.Commands;
using Microsoft.AspNetCore.Mvc;
using appDomain = Hotel.Booking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Hotel.Booking.Application.DTOs;

namespace Hotel.Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
             BookingResponseDTO details = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBookingById), new { id = details.BookingId }, details);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<appDomain.Booking>>> GetAllBookings()
        {
            GetAllBookingsQuery query = new GetAllBookingsQuery();
            List<appDomain.Booking> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<appDomain.Booking>> GetBookingById(Guid id)
        {
            GetBookingByIdQuery query = new GetBookingByIdQuery { Id = id };
            appDomain.Booking result = await _mediator.Send(query);

            if (result is null)
            {
                return NotFound(new { message = "Booking not found" });
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<appDomain.Booking>> UpdateBooking(Guid id, [FromBody] UpdateBookingCommand command)
        {
            try
            {
                command.Id = id;
                appDomain.Booking booking = await _mediator.Send(command);
                return Ok(booking);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            try
            {
                DeleteBookingCommand command = new DeleteBookingCommand { Id = id };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpGet("customer/{customerId}")]
        [Authorize]
        public async Task<ActionResult<List<appDomain.Booking>>> GetBookingsByCustomerId(Guid customerId)
        {
            try
            {
            GetBookingByCustomerIdQuery query = new GetBookingByCustomerIdQuery { CustomerId = customerId };
            List<appDomain.Booking> bookings = await _mediator.Send(query);
            return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
