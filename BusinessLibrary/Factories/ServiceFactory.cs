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
            Quantity = form.Quantity,
            
        };
    }

    public static Service Create(ServiceEntity entity)
    {
        return new Service()
        {
            Id = entity.Id,
            Name = entity.Name,
            PricePerUnit = entity.PricePerUnit,
            Quantity = entity.Quantity,
            Unit = UnitTypeFactory.Create(entity.Unit)
        };
    }
}
