
using Hotel.Booking.Application.Query.Payment;
using MediatR;
using Hotel.Booking.Application.Command.Payment.Commands;



using Microsoft.AspNetCore.Mvc;
using domain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand command)
        {
            Guid id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPaymentById), new { id }, new { id });
        }

        [HttpGet]
        public async Task<ActionResult<List<domain.Payment>>> GetAllPayments()
        {
            GetAllPaymentsQuery query = new GetAllPaymentsQuery();
            List<domain.Payment> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<domain.Payment>> GetPaymentById(Guid id)
        {
            GetPaymentByIdQuery query = new GetPaymentByIdQuery { Id = id };
            domain.Payment? result = await _mediator.Send(query);

            if (result is null)
            {
                return NotFound(new { message = "Payment not found" });
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<domain.Payment>> UpdatePayment(Guid id, [FromBody] UpdatePaymentCommand command)
        {
            try
            {
                command.Id = id;
                domain.Payment payment = await _mediator.Send(command);
                return Ok(payment);
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
        public async Task<IActionResult> DeletePayment(Guid id)
        {
            try
            {
                DeletePaymentCommand command = new DeletePaymentCommand { Id = id };
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
