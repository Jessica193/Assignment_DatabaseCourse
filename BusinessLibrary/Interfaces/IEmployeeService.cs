using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface IEmployeeService
{
    Task<bool> CreateAsync(EmployeeRegistrationForm form);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<IEnumerable<Employee>> GetAllEmployeesWithRoleAsync();
    Task<Employee?> GetEmployeeByIdAsync(int id);
    Task<Employee?> GetEmployeeWithRoleByIdAsync(int id);
    Task<bool> UpdateEmployeeAsync(int id, EmployeeUpdateForm form);
    Task<bool> DeleteEmployeeAsync(int id);
}






