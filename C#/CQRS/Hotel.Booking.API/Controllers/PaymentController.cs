
using Hotel.Booking.Application.Query.Payment;
using MediatR;
using Hotel.Booking.Application.Command.Payment.Commands;



using Microsoft.AspNetCore.Mvc;
using appDomain = Hotel.Booking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<List<appDomain.Payment>>> GetAllPayments()
        {
            GetAllPaymentsQuery query = new GetAllPaymentsQuery();
            List<appDomain.Payment> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<appDomain.Payment>> GetPaymentById(Guid id)
        {
            GetPaymentByIdQuery query = new GetPaymentByIdQuery { Id = id };
            appDomain.Payment? result = await _mediator.Send(query);

            if (result is null)
            {
                return NotFound(new { message = "Payment not found" });
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<appDomain.Payment>> UpdatePayment(Guid id, [FromBody] UpdatePaymentCommand command)
        {
            try
            {
                command.Id = id;
                appDomain.Payment payment = await _mediator.Send(command);
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

        [HttpPost("confirm")]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmPaymentCommand command)
        {
            Console.WriteLine("=== PaymentController.ConfirmPayment START ===");
            try
            {
            // Log the received values for debugging
           
            
            bool result = await _mediator.Send(command);
            if (result)
            {
                Console.WriteLine("SUCCESS: Payment confirmed");
                return Ok(new { message = "Payment confirmed successfully" });
            }
            else
            {
                Console.WriteLine("FAILURE: Payment confirmation returned false");
                return BadRequest(new { message = "Payment confirmation failed" });
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION in ConfirmPayment: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                Console.WriteLine("=== PaymentController.ConfirmPayment END (ERROR) ===");
                return StatusCode(500, new { 
                    message = ex.Message,
                    details = ex.InnerException?.Message ?? "No inner exception"
                });
                
            }
            
        }
    }
}
