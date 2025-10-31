using HotelBookingSystem.Context;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class CustomerServices : ICustomer
    {
        private readonly HotelBookingSystemContext _context;

        public CustomerServices(HotelBookingSystemContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Bookings)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            return customer;
        }

        public async Task<IEnumerable<Booking>> GetCustomerBookings(int customerId)
        {
            return await _context.Bookings
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task RemoveCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(customer.Id);
            if (existingCustomer == null)
                throw new KeyNotFoundException("Customer not found");

            existingCustomer.FullName = customer.FullName;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.IdProofNumber = customer.IdProofNumber;

            await _context.SaveChangesAsync();
            return existingCustomer;
        }
    }
}