using HotelBookingSystem.DTO;
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
        public async Task<ActionResult<RoomResponseDTO>> AddRoom(RoomCreateDTO room)
        {
            try
            {
                RoomResponseDTO newRoom = await _services.AddRoom(room);
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
                IEnumerable<RoomResponseDTO> rooms = await _services.GetAllRooms();
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
                RoomResponseDTO room = await _services.GetRoomById(id);
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
                IEnumerable<RoomResponseDTO> rooms = await _services.GetRoomsByHotel(hotelId);
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
                IEnumerable<RoomResponseDTO> rooms = await _services.GetAvailableRooms(checkIn, checkOut, hotelId);
                return Ok(rooms);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoomResponseDTO>> UpdateRoom(int id, RoomUpdateDTO room)
        {
            try
            {
                RoomResponseDTO updatedRoom = await _services.UpdateRoom(id, room);
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
                RoomResponseDTO updatedRoom = await _services.UpdateRoomStatus(id, status);
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