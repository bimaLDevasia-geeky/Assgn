using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReview _services;

        public ReviewController(IReview services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<ActionResult<Review>> AddReview(Review review)
        {
            try
            {
                var newReview = await _services.AddReview(review);
                return CreatedAtAction(nameof(GetReviewById), new { id = newReview.Id }, newReview);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
        {
            try
            {
                var reviews = await _services.GetAllReviews();
                return Ok(reviews);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReviewById(int id)
        {
            try
            {
                var review = await _services.GetReviewById(id);
                return Ok(review);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Review not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByHotel(int hotelId)
        {
            try
            {
                var reviews = await _services.GetReviewsByHotel(hotelId);
                return Ok(reviews);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByCustomer(int customerId)
        {
            try
            {
                var reviews = await _services.GetReviewsByCustomer(customerId);
                return Ok(reviews);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("hotel/{hotelId}/average-rating")]
        public async Task<ActionResult<double>> GetAverageRatingForHotel(int hotelId)
        {
            try
            {
                var averageRating = await _services.GetAverageRatingForHotel(hotelId);
                return Ok(new { AverageRating = averageRating });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Review>> UpdateReview(int id, Review review)
        {
            if (id != review.Id)
                return BadRequest(new { Message = "ID mismatch" });

            try
            {
                var updatedReview = await _services.UpdateReview(review);
                return Ok(updatedReview);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Review not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            try
            {
                await _services.RemoveReview(id);
                return Ok(new { Message = "Review deleted successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Review not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }
}