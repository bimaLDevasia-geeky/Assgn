using HotelBookingSystem.Context;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class EmployeeServices : IEmployee
    {
        private readonly HotelBookingSystemContext _context;

        public EmployeeServices(HotelBookingSystemContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            // Verify if hotel exists
            var hotel = await _context.Hotels.FindAsync(employee.HotelId);
            if (hotel == null)
                throw new KeyNotFoundException("Hotel not found");

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _context.Employees
                .Include(e => e.Hotel)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Hotel)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByHotel(int hotelId)
        {
            return await _context.Employees
                .Where(e => e.HotelId == hotelId)
                .Include(e => e.Hotel)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByRole(string role)
        {
            return await _context.Employees
                .Where(e => e.Role.ToLower() == role.ToLower())
                .Include(e => e.Hotel)
                .ToListAsync();
        }

        public async Task RemoveEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(employee.Id);
            if (existingEmployee == null)
                throw new KeyNotFoundException("Employee not found");

            // Verify if new hotel exists if hotel is being changed
            if (existingEmployee.HotelId != employee.HotelId)
            {
                var hotel = await _context.Hotels.FindAsync(employee.HotelId);
                if (hotel == null)
                    throw new KeyNotFoundException("Hotel not found");
            }

            existingEmployee.FullName = employee.FullName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Role = employee.Role;
            existingEmployee.HotelId = employee.HotelId;

            await _context.SaveChangesAsync();
            return existingEmployee;
        }
    }
}