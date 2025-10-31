using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoom _services;

        public RoomController(IRoom services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<ActionResult<Room>> AddRoom(Room room)
        {
            try
            {
                var newRoom = await _services.AddRoom(room);
                return CreatedAtAction(nameof(GetRoomById), new { id = newRoom.Id }, newRoom);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { Message = "Failed to add room" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
        {
            try
            {
                var rooms = await _services.GetAllRooms();
                return Ok(rooms);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoomById(int id)
        {
            try
            {
                var room = await _services.GetRoomById(id);
                return Ok(room);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Room not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByHotel(int hotelId)
        {
            try
            {
                var rooms = await _services.GetRoomsByHotel(hotelId);
                return Ok(rooms);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Room>>> GetAvailableRooms([FromQuery] DateTime checkIn, [FromQuery] DateTime checkOut, [FromQuery] int hotelId)
        {
            try
            {
                var rooms = await _services.GetAvailableRooms(checkIn, checkOut, hotelId);
                return Ok(rooms);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Room>> UpdateRoom(int id, Room room)
        {
            if (id != room.Id)
                return BadRequest(new { Message = "ID mismatch" });

            try
            {
                var updatedRoom = await _services.UpdateRoom(room);
                return Ok(updatedRoom);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Room not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<Room>> UpdateRoomStatus(int id, RoomStatus status)
        {
            try
            {
                var updatedRoom = await _services.UpdateRoomStatus(id, status);
                return Ok(updatedRoom);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Room not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            try
            {
                await _services.RemoveRoom(id);
                return Ok(new { Message = "Room deleted successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Room not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }
}