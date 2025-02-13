using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

namespace BusinessLibrary.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm Create()
    {
        return new ProjectRegistrationForm();
    }

    public static ProjectEntity Create(ProjectRegistrationForm form)
    {
        return new ProjectEntity()
        {
            Name = form.Name,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            QuantityofServiceUnits = form.QuantityofServiceUnits,
            // TotalPrice = form.QuantityofServiceUnits * form.Service.PricePerUnit Går inte för jag har inte tillgång till Service
        };
    }

    public static Project Create(ProjectEntity entity)
    {

        return new Project()
        {
            Id = entity.Id,
            Name= entity.Name,
            Description= entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            QuantityofServiceUnits= entity.QuantityofServiceUnits,
            TotalPrice = entity.QuantityofServiceUnits * entity.Service.PricePerUnit, //Den här vill jag ha redan när jag sparar ner entiteten
            StatusType = StatusTypeFactory.Create(entity.StatusType),
            Service = ServiceFactory.Create(entity.Service),
            Employee = EmployeeFactory.Create(entity.Employee),
            Customer = CustomerFactory.Create(entity.Customer),
        };
    }

    public static ProjectEntity CreateUpdatedEntity(ProjectUpdateForm form, ProjectEntity entity)
    {
        return new ProjectEntity()
        {
            Id = entity.Id,
            Name = form.Name,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            QuantityofServiceUnits = form.QuantityofServiceUnits,
            TotalPrice = form.QuantityofServiceUnits * entity.Service.PricePerUnit,
        };
    }

}
