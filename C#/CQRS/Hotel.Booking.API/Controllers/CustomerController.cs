
using Hotel.Booking.Application.Query.Customer;
using MediatR;
using Hotel.Booking.Application.Command.Customer.Commands;



using Microsoft.AspNetCore.Mvc;
using appDomain = Hotel.Booking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<appDomain.Customer>>> GetAllCustomers()
        {
            GetAllCustomersQuery query = new GetAllCustomersQuery();
            List<appDomain.Customer> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin,Customer")]
        public async Task<ActionResult<appDomain.Customer>> GetCustomerById(Guid id)
        {
            GetCustomerByIdQuery query = new GetCustomerByIdQuery { Id = id };
            appDomain.Customer? result = await _mediator.Send(query);

            if (result is null)
            {
                return NotFound(new { message = "Customer not found" });
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<appDomain.Customer>> UpdateCustomer(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            try
            {
                command.Id = id;
                appDomain.Customer customer = await _mediator.Send(command);
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
        [Authorize(Roles ="Admin")]
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
