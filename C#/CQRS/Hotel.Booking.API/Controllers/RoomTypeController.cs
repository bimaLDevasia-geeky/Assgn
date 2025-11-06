using Hotel.Booking.Application.Command;
using Hotel.Booking.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using domain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomTypeController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateRoomType([FromBody] CreateRoomTypeCommand command)
        {
            Guid id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRoomTypeById), new { id }, new { id });
        }

        [HttpGet]
        public async Task<ActionResult<List<domain.RoomType>>> GetAllRoomTypes()
        {
            GetAllRoomTypesQuery query = new GetAllRoomTypesQuery();
            List<domain.RoomType> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<domain.RoomType>> GetRoomTypeById(Guid id)
        {
            GetRoomTypeByIdQuery query = new GetRoomTypeByIdQuery { Id = id };
            domain.RoomType? result = await _mediator.Send(query);

            if (result is null)
            {
                return NotFound(new { message = "RoomType not found" });
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<domain.RoomType>> UpdateRoomType(Guid id, [FromBody] UpdateRoomTypeCommand command)
        {
            try
            {
                command.Id = id;
                domain.RoomType roomType = await _mediator.Send(command);
                return Ok(roomType);
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
        public async Task<IActionResult> DeleteRoomType(Guid id)
        {
            try
            {
                DeleteRoomTypeCommand command = new DeleteRoomTypeCommand { Id = id };
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
