using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;
using System.Runtime.Serialization;

namespace BusinessLibrary.Factories;

public static class ContactPersonFactory
{
    public static ContactPersonRegistrationForm Create()
    {
        return new ContactPersonRegistrationForm();
    }

    public static ContactPersonEntity Create(ContactPersonRegistrationForm form)
    {
        return new ContactPersonEntity()
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            PhoneNumber = form.PhoneNumber,
            CustomerId = form.CustomerId,
        };
    }

    public static ContactPerson Create(ContactPersonEntity entity)
    {

        return new ContactPerson()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            CustomerId= entity.CustomerId,

            //Nytt
            Customer = CustomerFactory.Create(entity.Customer),
        };
    }

    public static ContactPersonEntity CreateUpdatedEntity(ContactPersonUpdateForm form, ContactPersonEntity entity)
    {
        return new ContactPersonEntity()
        {
            Id = entity.Id,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            PhoneNumber = form.PhoneNumber,
            CustomerId = entity.CustomerId,
        };
    }
}
