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
            RoleId = form.RoleId,
        };
    }

    public static Employee Create(EmployeeEntity entity)
    {
        var projects = new List<Project>();

        foreach (var row in entity.Projects)
        {
            projects.Add(new Project()
            {
                Name = row.Name,
                Description = row.Description,
                StartDate = row.StartDate,
                EndDate = row.EndDate,
                QuantityofServiceUnits = row.QuantityofServiceUnits,
                TotalPrice = row.TotalPrice, //behövs något göras här?
                CustomerId = row.CustomerId,
                StatusTypeId = row.StatusTypeId,
                EmployeeId = row.EmployeeId,
                ServiceId = row.ServiceId
            });
        }


        return new Employee()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            RoleId = entity.RoleId,
            Projects = projects,
            Role = RoleFactory.Create(entity.Role),
        };
    }

    public static EmployeeEntity CreateUpdatedEntity(EmployeeUpdateForm form, EmployeeEntity entity)
    {
        return new EmployeeEntity()
        {
            Id = entity.Id,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            RoleId = entity.RoleId,
        };
    }
}
