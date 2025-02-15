using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

namespace BusinessLibrary.Factories;

public static class RoleFactory
{
    public static RoleRegistrationForm Create()
    {
        return new RoleRegistrationForm();
    }

    public static RoleEntity Create(RoleRegistrationForm form)
    {
        return new RoleEntity()
        {
            Name = form.Name,
        };
    }

    public static Role Create(RoleEntity entity)
    {
        var employees = new List<Employee>();

        foreach (var row in entity.Employees)
        {
            employees.Add(new Employee()
            {
                Id = row.Id,
                FirstName = row.FirstName,
                LastName = row.LastName,
                Email = row.Email,
                RoleId = row.RoleId,
            });
        }


        return new Role()
        {
            Id = entity.Id,
            Name = entity.Name,
            Employees = employees
        };
    }

    public static RoleEntity CreateUpdatedEntity(RoleUpdateForm form, RoleEntity entity)
    {
        return new RoleEntity()
        {
            Id = entity.Id,
            Name = form.Name,
        };
    }
}
