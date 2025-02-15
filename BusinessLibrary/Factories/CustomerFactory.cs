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
        var contactPersons = new List<ContactPerson>();

        foreach (var row in entity.ContactPersons)
            contactPersons.Add(new ContactPerson
            {
                FirstName = row.FirstName,
                LastName = row.LastName,
                Email = row.Email,
                PhoneNumber = row.PhoneNumber,
                CustomerId = row.CustomerId,
            });

        return new Customer()
        {
            Id = entity.Id,
            Name = entity.Name,
            ContactPersons = contactPersons
        };
    }

    public static CustomerEntity CreateUpdatedEntity(CustomerUpdateForm form, CustomerEntity entity)
    {
        return new CustomerEntity()
        {
            Id = entity.Id,
            Name=form.Name
        };
    }
}
