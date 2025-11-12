using MediatR;
using Hotel.Booking.Application.Command.Room.Commands;
using Hotel.Booking.Application.Query.Room;


using Microsoft.AspNetCore.Mvc;
using domain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Application;

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
            GetAllRoomsQuery query = new GetAllRoomsQuery();
            List<domain.Room> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<domain.Room>> GetRoomById(Guid id)
        {
            GetRoomByIdQuery query = new GetRoomByIdQuery { Id = id };
            domain.Room? result = await _mediator.Send(query);

            if (result is null)
            {
                return NotFound(new { message = "Room not found" });
            }

            return Ok(result);
        }


        [HttpGet("byhotel")]
        public async Task<ActionResult<List<domain.Room>>> GetRoomsByHotelId([FromQuery] Guid hotelId)
        {
            GetRoomsByHostelIdQuery query = new GetRoomsByHostelIdQuery { HotelId = hotelId };
            List<domain.Room> result = await _mediator.Send(query);
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
                DeleteRoomCommand command = new DeleteRoomCommand { Id = id };
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
