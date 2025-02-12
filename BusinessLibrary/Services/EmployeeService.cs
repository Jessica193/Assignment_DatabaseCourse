using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BusinessLibrary.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<bool> Create(EmployeeRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.FirstName) || string.IsNullOrWhiteSpace(form.LastName) || string.IsNullOrWhiteSpace(form.Email)) return false;

        var result = await _employeeRepository.ExistsAsync(x => x.Email.ToLower() == form.Email.ToLower());
        if (result)
        {
            return false;
        }
        try
        {
            await _employeeRepository.CreateAsync(EmployeeFactory.Create(form));
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating employee entity :: {ex.Message}");
            return false;
        }
    }


    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        var entities = await _employeeRepository.GetAllAsync();
        var employees = entities.Select(EmployeeFactory.Create).ToList();
        return employees;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesWithRole()
    {
        var entities = await _employeeRepository.GetAllWithDetailsAsync(query => query.Include(e => e.Role));
        var employees = entities.Select(EmployeeFactory.Create).ToList();
        return employees;
    }

    public async Task<Employee?> GetEmployeeById(int id)
    {
        var result = await _employeeRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _employeeRepository.GetOneAsync(x => x.Id == id);
            var employee = EmployeeFactory.Create(entity);
            return employee;
        }
        return null;
    }

    public async Task<Employee?> GetEmployeeWithRoleById(int id)
    {
        var result = await _employeeRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _employeeRepository.GetOneWithDetailsAsync(query => query.Include(e => e.Role), x => x.Id == id);
            var employee = EmployeeFactory.Create(entity);
            return employee;
        }
        return null;
    }

    public async Task<bool> UpdateEmployee(int id, EmployeeUpdateForm form)
    {
        var entity = await _employeeRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        var updatedEntity = EmployeeFactory.CreateUpdatedEntity(form, entity);

        try
        {
            await _employeeRepository.UpdateAsync(updatedEntity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating employee entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        var entity = await _employeeRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;
        try
        {
            await _employeeRepository.DeleteAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting employee entity :: {ex.Message}");
            return false;
        }
    }
}
