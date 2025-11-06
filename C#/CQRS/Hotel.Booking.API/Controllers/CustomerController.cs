using Hotel.Booking.Application.Command;
using Hotel.Booking.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using domain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            Guid id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCustomerById), new { id }, new { id });
        }

        [HttpGet]
        public async Task<ActionResult<List<domain.Customer>>> GetAllCustomers()
        {
            GetAllCustomersQuery query = new GetAllCustomersQuery();
            List<domain.Customer> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<domain.Customer>> GetCustomerById(Guid id)
        {
            GetCustomerByIdQuery query = new GetCustomerByIdQuery { Id = id };
            domain.Customer result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(new { message = "Customer not found" });
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<domain.Customer>> UpdateCustomer(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            try
            {
                command.Id = id;
                domain.Customer customer = await _mediator.Send(command);
                return Ok(customer);
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
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                DeleteCustomerCommand command = new DeleteCustomerCommand { Id = id };
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
