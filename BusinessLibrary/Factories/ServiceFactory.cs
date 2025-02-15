using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

namespace BusinessLibrary.Factories;

public static class ServiceFactory
{
    public static ServiceRegistrationForm Create()
    {
        return new ServiceRegistrationForm();
    }

    public static ServiceEntity Create(ServiceRegistrationForm form)
    {
        return new ServiceEntity()
        {
            Name = form.Name,
            PricePerUnit = form.PricePerUnit,
            UnitTypeId = form.UnitTypeId,
        };
    }

    public static Service Create(ServiceEntity entity)
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


        return new Service()
        {
            Id = entity.Id,
            Name = entity.Name,
            PricePerUnit = entity.PricePerUnit,
            UnitTypeId = entity.UnitTypeId,
            Unit = UnitTypeFactory.Create(entity.Unit),
            Projects = projects
        };
    }

    public static ServiceEntity CreateUpdatedEntity(ServiceUpdateForm form, ServiceEntity entity)
    {
        return new ServiceEntity()
        {
            Id = entity.Id,
            Name = form.Name,
            PricePerUnit = form.PricePerUnit,
            UnitTypeId = entity.UnitTypeId
        };
    }
}
