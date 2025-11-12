
using Hotel.Booking.Application.DTOs;
using MediatR;
using Hotel.Booking.Application.Command.Hotel.Commands;
using Hotel.Booking.Application.Query.Hotel;

using Microsoft.AspNetCore.Mvc;
using appDomain = Hotel.Booking.Domain.Entities;
using Hotel.Booking.Application.Query;

namespace Hotel.Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<appDomain.Hotel>>> GetAllHotels()
        {
            GetAllHotelQuery query = new GetAllHotelQuery();
            List<appDomain.Hotel> hotels =  await _mediator.Send(query);
            return Ok(hotels);
        }


        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelCommand command)
        {
            Guid Id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetHotel), new { id = Id }, new { id = Id });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<appDomain.Hotel>> GetHotel(Guid id)
        {
            GetHotelByIdQuery query = new GetHotelByIdQuery { Id = id };
            appDomain.Hotel hotel = await _mediator.Send(query);
            return Ok(hotel);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<appDomain.Hotel>> UpdateHotel(Guid id, [FromBody] UpdateHotelCommand command)
        {
            try
            {
                command.Id = id;
                appDomain.Hotel hotel = await _mediator.Send(command);
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            DeleteHotelCommand command = new DeleteHotelCommand { Id = id };
            bool result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound(new { message = "Hotel not found" });
            }
            return NoContent();
        }


    }

}
