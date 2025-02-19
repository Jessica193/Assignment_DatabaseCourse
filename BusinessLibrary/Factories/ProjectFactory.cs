using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace BusinessLibrary.Factories;

public static class ProjectFactory
{
   
    public static ProjectRegistrationForm Create()
    {
        return new ProjectRegistrationForm();
    }

    public static async Task<ProjectEntity> CreateAsync(ProjectRegistrationForm form, IServiceRepository serviceRepository)
    {
        //Hjälp från chatGPT4o för att få fram TotalPrice
        var service = await serviceRepository.GetOneAsync(s => s.Id == form.ServiceId);
        if (service == null) throw new Exception("Service not found");


        return new ProjectEntity()
        {
            Name = form.Name,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            QuantityofServiceUnits = form.QuantityofServiceUnits,
            TotalPrice = form.QuantityofServiceUnits * service.PricePerUnit,
            CustomerId = form.CustomerId,
            EmployeeId = form.EmployeeId,
            ServiceId = form.ServiceId,
            StatusTypeId = form.StatusTypeId,
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
            TotalPrice = entity.TotalPrice,
            CustomerId = entity.CustomerId,
            EmployeeId = entity.EmployeeId,
            ServiceId = entity.ServiceId,
            StatusTypeId = entity.StatusTypeId,

            StatusType = StatusTypeFactory.Create(entity.StatusType),
            Service = ServiceFactory.Create(entity.Service),
            Employee = EmployeeFactory.Create(entity.Employee),
            Customer = CustomerFactory.Create(entity.Customer),
        };
    }

    public static void CreateUpdatedEntity(ProjectUpdateForm form, ProjectEntity entity)
    {
        entity.Name = form.Name;
        entity.Description = form.Description;
        entity.StartDate = form.StartDate;
        entity.EndDate = form.EndDate;
        entity.QuantityofServiceUnits = form.QuantityofServiceUnits;
        entity.TotalPrice = form.TotalPrice;
    }

}
