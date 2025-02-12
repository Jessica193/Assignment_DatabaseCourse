using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface IEmployeeService
{
    Task<bool> Create(EmployeeRegistrationForm form);
    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<IEnumerable<Employee>> GetAllEmployeesWithRole();
    Task<Employee?> GetEmployeeById(int id);
    Task<Employee?> GetEmployeeWithRoleById(int id);
    Task<bool> UpdateEmployee(int id, EmployeeUpdateForm form);
    Task<bool> DeleteEmployee(int id);
}






