using BusinessLibrary.Dtos;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;

namespace BusinessLibrary.Services;

public class CustomerService : ICustomerService
{
    public Task<bool> Create(CustomerRegistrationForm form)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCustomer(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetAllCustomer()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetAllCustomerWithContactPersons()
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetCustomerById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetCustomerWithContactPersonsById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCustomer(int id, CustomerUpdateForm form)
    {
        throw new NotImplementedException();
    }
}
