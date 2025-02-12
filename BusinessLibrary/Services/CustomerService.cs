using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BusinessLibrary.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<bool> Create(CustomerRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.Name)) return false;

        var result = await _customerRepository.ExistsAsync(x => x.Name.ToLower() == form.Name.ToLower());
        if (result)
        {
            return false;
        }
        try
        {
            await _customerRepository.CreateAsync(CustomerFactory.Create(form));
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating customer entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<Customer>> GetAllCustomer()
    {
        var entities = await _customerRepository.GetAllAsync();
        var customers = entities.Select(CustomerFactory.Create).ToList();
        return customers;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomerWithContactPersons()
    {
        var entities = await _customerRepository.GetAllWithDetailsAsync(query => query.Include(c => c.ContactPerson));
        var customers = entities.Select(CustomerFactory.Create).ToList();
        return customers;
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        var result = await _customerRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _customerRepository.GetOneAsync(x => x.Id == id);
            var customer = CustomerFactory.Create(entity);
            return customer;
        }
        return null;
    }

    public async Task<Customer?> GetCustomerWithContactPersonsById(int id)
    {
        var result = await _customerRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _customerRepository.GetOneWithDetailsAsync(query => query.Include(c => c.ContactPerson), x => x.Id == id);
            var customer = CustomerFactory.Create(entity);
            return customer;
        }
        return null;
    }

    public async Task<bool> UpdateCustomer(int id, CustomerUpdateForm form)
    {
        var entity = await _customerRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        var updatedEntity = CustomerFactory.CreateUpdatedEntity(form, entity);

        try
        {
            await _customerRepository.UpdateAsync(updatedEntity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating customer entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteCustomer(int id)
    {
        var entity = await _customerRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;
        try
        {
            await _customerRepository.DeleteAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting customer entity :: {ex.Message}");
            return false;
        }
    }
}
