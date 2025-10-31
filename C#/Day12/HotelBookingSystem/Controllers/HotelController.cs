using HotelBookingSystem.DTO;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelServices _services;

        public HotelController(IHotelServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult> GetHotels()
        {
            IEnumerable<Hotel> hotels = await _services.GetAllHotels();
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetSpecificHotel(int id)
        {
            Hotel hotel = await _services.GetHotelById(id);
            return Ok(hotel);
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> AddHotel(HotelRequest body)
        {
             Hotel hot = await _services.AddHotel(body);
            return Ok(hot);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Hotel>> UpdateHotel(HotelRequest hotel,int id)
        {
            try
            {
            var changedHotel = await _services.UpdateHotel(hotel,id);
            return Ok(changedHotel);

            }catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message =ex.Message });
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, new { Message = "Error when updating the Database" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            try
            {
           bool done= await _services.DeleteHotel(id);
               return NoContent();
            }
            catch(KeyNotFoundException E) {
                return NotFound(new { Message=E.Message});
            }
            catch(Exception ex)
            {
                return StatusCode(500, new {Message = ex.Message});
            }
        }
    }
}
