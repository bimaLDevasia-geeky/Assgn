using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _services;

        public EmployeeController(IEmployee services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            try
            {
                var newEmployee = await _services.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, newEmployee);
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
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            try
            {
                var employees = await _services.GetAllEmployees();
                return Ok(employees);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _services.GetEmployeeById(id);
                return Ok(employee);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Employee not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByHotel(int hotelId)
        {
            try
            {
                var employees = await _services.GetEmployeesByHotel(hotelId);
                return Ok(employees);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("role/{role}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByRole(string role)
        {
            try
            {
                var employees = await _services.GetEmployeesByRole(role);
                return Ok(employees);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest(new { Message = "ID mismatch" });

            try
            {
                var updatedEmployee = await _services.UpdateEmployee(employee);
                return Ok(updatedEmployee);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _services.RemoveEmployee(id);
                return Ok(new { Message = "Employee deleted successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Employee not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }
}