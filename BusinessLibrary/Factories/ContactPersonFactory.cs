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
            Customer = CustomerFactory.Create(entity.Customer),
        };
    }

    public static void UpdateEntity(ContactPersonUpdateForm form, ContactPersonEntity entity)
    {
        entity.FirstName = form.FirstName;
        entity.LastName = form.LastName;
        entity.Email = form.Email;
        entity.PhoneNumber = form.PhoneNumber;
}
}
