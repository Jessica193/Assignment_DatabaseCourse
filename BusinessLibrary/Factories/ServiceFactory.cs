﻿using BusinessLibrary.Dtos;
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
        };
    }

    public static Service Create(ServiceEntity entity)
    {
        return new Service()
        {
            Id = entity.Id,
            Name = entity.Name,
            PricePerUnit = entity.PricePerUnit,
            Unit = UnitTypeFactory.Create(entity.Unit)
        };
    }

    public static ServiceEntity CreateUpdatedEntity(ServiceUpdateForm form, ServiceEntity entity)
    {
        return new ServiceEntity()
        {
            Id = entity.Id,
            Name = form.Name,
            PricePerUnit = form.PricePerUnit,
        };
    }
}
