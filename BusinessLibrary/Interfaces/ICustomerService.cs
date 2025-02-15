using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface ICustomerService
{
    Task<bool> CreateAsync(CustomerRegistrationForm form);
    Task<IEnumerable<Customer>> GetAllCustomerAsync();
    Task<IEnumerable<Customer>> GetAllCustomerWithContactPersonsAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer?> GetCustomerWithContactPersonsByIdAsync(int id);
    Task<bool> UpdateCustomerAsync(int id, CustomerUpdateForm form);
    Task<bool> DeleteCustomerAsync(int id);
}






