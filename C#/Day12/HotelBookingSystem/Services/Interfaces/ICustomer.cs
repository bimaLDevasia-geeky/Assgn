using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.Interfaces
{
    public interface ICustomer
    {
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> GetCustomerById(int id);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> UpdateCustomer(Customer customer);
        Task RemoveCustomer(int id);
        Task<IEnumerable<Booking>> GetCustomerBookings(int customerId);
    }
}