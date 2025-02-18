using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

namespace BusinessLibrary.Factories;

public static class StatusTypeFactory
{
    public static StatusTypeRegistrationForm Create()
    {
        return new StatusTypeRegistrationForm();
    }

    public static StatusTypeEntity Create(StatusTypeRegistrationForm form)
    {
        return new StatusTypeEntity()
        {
            Status = form.Status,
        };
    }

    public static StatusType Create(StatusTypeEntity entity)
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
                TotalPrice = row.TotalPrice,
                CustomerId = row.CustomerId,
                StatusTypeId = row.StatusTypeId,
                EmployeeId = row.EmployeeId,
                ServiceId = row.ServiceId
            });
        }


        return new StatusType()
        {
            Id = entity.Id,
            Status = entity.Status,
            Projects = projects
        };
    }

    public static void CreateUpdatedEntity(StatusTypeUpdateForm form, StatusTypeEntity entity)
    {
        entity.Status = form.Status;
    }
}
