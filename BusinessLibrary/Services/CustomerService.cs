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

    public async Task<bool> CreateAsync(CustomerRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.Name)) return false;

        var result = await _customerRepository.ExistsAsync(x => x.Name.ToLower() == form.Name.ToLower());
        if (result)
        {
            return false;
        }

        await _customerRepository.BeginTransactionAsync();

        try
        {
            await _customerRepository.CreateAsync(CustomerFactory.Create(form));
            await _customerRepository.SaveToDatabaseAsync();
            await _customerRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating customer entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<Customer>> GetAllCustomerAsync()
    {
        var entities = await _customerRepository.GetAllAsync();
        var customers = entities.Select(CustomerFactory.Create).ToList();
        return customers;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomerWithContactPersonsAsync()
    {
        var entities = await _customerRepository.GetAllWithDetailsAsync(query => query.Include(c => c.ContactPersons));
        var customers = entities.Select(CustomerFactory.Create).ToList();
        return customers;
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
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

    public async Task<Customer?> GetCustomerWithContactPersonsByIdAsync(int id)
    {
        var result = await _customerRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _customerRepository.GetOneWithDetailsAsync(query => query.Include(c => c.ContactPersons), x => x.Id == id);
            var customer = CustomerFactory.Create(entity);
            return customer;
        }
        return null;
    }

    public async Task<bool> UpdateCustomerAsync(int id, CustomerUpdateForm form)
    {
        var entity = await _customerRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        CustomerFactory.UpdateEntity(form, entity);

        await _customerRepository.BeginTransactionAsync();

        try
        {
            _customerRepository.Update(entity);
            await _customerRepository.SaveToDatabaseAsync();
            await _customerRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating customer entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var entity = await _customerRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        await _customerRepository.BeginTransactionAsync();

        try
        {
            _customerRepository.Delete(entity);
            await _customerRepository.SaveToDatabaseAsync();
            await _customerRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting customer entity :: {ex.Message}");
            return false;
        }
    }
}
