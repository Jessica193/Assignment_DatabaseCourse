using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

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
        };
    }
}
