using BusinessLibrary.Dtos;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;

namespace BusinessLibrary.Services;

public class EmployeeService : IEmployeeService
{
    public Task<bool> Create(EmployeeRegistrationForm form)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteEmployee(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Employee>> GetAllEmployees()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Employee>> GetAllEmployeesWithRole()
    {
        throw new NotImplementedException();
    }

    public Task<Employee?> GetEmployeeById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Employee?> GetEmployeeWithRoleById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateEmployee(int id, EmployeeUpdateForm form)
    {
        throw new NotImplementedException();
    }
}
