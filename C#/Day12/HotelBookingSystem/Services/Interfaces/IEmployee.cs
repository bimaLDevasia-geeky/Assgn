using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.Interfaces
{
    public interface IEmployee
    {
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> GetEmployeeById(int id);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<IEnumerable<Employee>> GetEmployeesByHotel(int hotelId);
        Task<IEnumerable<Employee>> GetEmployeesByRole(string role);
        Task<Employee> UpdateEmployee(Employee employee);
        Task RemoveEmployee(int id);
    }
}