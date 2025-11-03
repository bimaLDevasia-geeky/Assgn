using HotelBookingSystem.DTO;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomTypeController:ControllerBase
    {
        private readonly IRoomType _services;

        public RoomTypeController(IRoomType service)
        {
            _services = service;
        }

        [HttpPost]
        public async Task<ActionResult<RoomTypeResponseDTO>> AddRoomType(RoomTypeCreateDTO request)
        {
            try
            {
                var roomType = await _services.AddRoomType(request);
                return CreatedAtAction(nameof(GetRoomTypeById), new { id = roomType.Id }, roomType);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { Message = "Failed to add room type" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypeResponseDTO>>> GetAllRoomTypes()
        {
            try
            {
                var roomTypes = await _services.GetAllRoomTypes();
                return Ok(roomTypes);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomType>> GetRoomTypeById(int id)
        {
            try
            {
                var roomType = await _services.GetRoomTypeById(id);
                return Ok(roomType);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Room type not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoomTypeResponseDTO>> UpdateRoomType(int id, RoomTypeUpdateDTO roomType)
        {
            try
            {
                var updatedRoomType = await _services.UpdateRoomType(id, roomType);
                return Ok(updatedRoomType);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Room type not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoomType(int id)
        {
            try
            {
                await _services.RemoveRoomType(id);
                return Ok(new { Message = "Room type deleted successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Room type not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }
}
