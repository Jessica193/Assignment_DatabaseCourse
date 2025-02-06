using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

namespace BusinessLibrary.Factories;

public static class CustomerFactory
{
    public static CustomerRegistrationForm Create()
    {
        return new CustomerRegistrationForm();
    }

    public static CustomerEntity Create(CustomerRegistrationForm form)
    {
        return new CustomerEntity()
        {
            Name = form.Name,
        };
    }

    public static Customer Create(CustomerEntity entity)
    {
        return new Customer()
        {
            Id = entity.Id,
            Name = entity.Name,

        };
    }
}
