using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _services;

        public CustomerController(ICustomer services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            try
            {
                var newCustomer = await _services.AddCustomer(customer);
                return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.Id }, newCustomer);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { Message = "Failed to add customer" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            try
            {
                var customers = await _services.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _services.GetCustomerById(id);
                return Ok(customer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Customer not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("{id}/bookings")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetCustomerBookings(int id)
        {
            try
            {
                var bookings = await _services.GetCustomerBookings(id);
                return Ok(bookings);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
                return BadRequest(new { Message = "ID mismatch" });

            try
            {
                var updatedCustomer = await _services.UpdateCustomer(customer);
                return Ok(updatedCustomer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Customer not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _services.RemoveCustomer(id);
                return Ok(new { Message = "Customer deleted successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Customer not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }
}