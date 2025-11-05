using Hotel.Booking.Application.Command;
using Hotel.Booking.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using domain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomCommand command)
        {
            Guid id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRoomById), new { id }, new { id });
        }

        [HttpGet]
        public async Task<ActionResult<List<domain.Room>>> GetAllRooms()
        {
            var query = new GetAllRoomsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<domain.Room>> GetRoomById(Guid id)
        {
            var query = new GetRoomByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            
            if (result == null)
            {
                return NotFound(new { message = "Room not found" });
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<domain.Room>> UpdateRoom(Guid id, [FromBody] UpdateRoomCommand command)
        {
            try
            {
                command.Id = id;
                domain.Room room = await _mediator.Send(command);
                return Ok(room);
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
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            try
            {
                var command = new DeleteRoomCommand { Id = id };
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
