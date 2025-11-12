using MediatR;
using Hotel.Booking.Application.Command.Employee.Commands;
using Hotel.Booking.Application.Query.Employee;

using Microsoft.AspNetCore.Mvc;
using domain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            Guid id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id }, new { id });
        }

        [HttpGet]
        public async Task<ActionResult<List<domain.Employee>>> GetAllEmployees()
        {
            GetAllEmployeesQuery query = new GetAllEmployeesQuery();
            List<domain.Employee> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<domain.Employee>> GetEmployeeById(Guid id)
        {
            GetEmployeeByIdQuery query = new GetEmployeeByIdQuery { Id = id };
            domain.Employee result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(new { message = "Employee not found" });
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<domain.Employee>> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeCommand command)
        {
            try
            {
                command.Id = id;
                domain.Employee employee = await _mediator.Send(command);
                return Ok(employee);
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
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                DeleteEmployeeCommand command = new DeleteEmployeeCommand { Id = id };
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
