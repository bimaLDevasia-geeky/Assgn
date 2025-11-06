using Hotel.Booking.Application.Command;
using Hotel.Booking.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using domain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
            Guid id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBookingById), new { id }, new { id });
        }

        [HttpGet]
        public async Task<ActionResult<List<domain.Booking>>> GetAllBookings()
        {
            GetAllBookingsQuery query = new GetAllBookingsQuery();
            List<domain.Booking> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<domain.Booking>> GetBookingById(Guid id)
        {
            GetBookingByIdQuery query = new GetBookingByIdQuery { Id = id };
            domain.Booking result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(new { message = "Booking not found" });
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<domain.Booking>> UpdateBooking(Guid id, [FromBody] UpdateBookingCommand command)
        {
            try
            {
                command.Id = id;
                domain.Booking booking = await _mediator.Send(command);
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
    }
}
