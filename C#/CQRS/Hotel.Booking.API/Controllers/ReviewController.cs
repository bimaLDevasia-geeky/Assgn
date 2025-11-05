using Hotel.Booking.Application.Command;
using Hotel.Booking.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using domain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
        {
            Guid id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetReviewById), new { id }, new { id });
        }

        [HttpGet]
        public async Task<ActionResult<List<domain.Review>>> GetAllReviews()
        {
            var query = new GetAllReviewsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<domain.Review>> GetReviewById(Guid id)
        {
            var query = new GetReviewByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            
            if (result == null)
            {
                return NotFound(new { message = "Review not found" });
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<domain.Review>> UpdateReview(Guid id, [FromBody] UpdateReviewCommand command)
        {
            try
            {
                command.Id = id;
                domain.Review review = await _mediator.Send(command);
                return Ok(review);
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
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            try
            {
                var command = new DeleteReviewCommand { Id = id };
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
