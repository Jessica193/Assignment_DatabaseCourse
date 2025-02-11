using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

namespace BusinessLibrary.Factories;

public static class EmployeeFactory
{
    public static EmployeeRegistrationForm Create()
    {
        return new EmployeeRegistrationForm();
    }

    public static EmployeeEntity Create(EmployeeRegistrationForm form)
    {
        return new EmployeeEntity()
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
              
        };
    }

    public static Employee Create(EmployeeEntity entity)
    {
        return new Employee()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Role = RoleFactory.Create(entity.Role),
        };
    }
}
