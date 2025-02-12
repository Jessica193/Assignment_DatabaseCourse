using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface ICustomerService
{
    Task<bool> Create(CustomerRegistrationForm form);
    Task<IEnumerable<Customer>> GetAllCustomer();
    Task<IEnumerable<Customer>> GetAllCustomerWithContactPersons();
    Task<Customer?> GetCustomerById(int id);
    Task<Customer?> GetCustomerWithContactPersonsById(int id);
    Task<bool> UpdateCustomer(int id, CustomerUpdateForm form);
    Task<bool> DeleteCustomer(int id);
}






