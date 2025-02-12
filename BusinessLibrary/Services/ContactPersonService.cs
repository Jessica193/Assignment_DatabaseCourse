using BusinessLibrary.Dtos;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;

namespace BusinessLibrary.Services;

public class ContactPersonService : IContactPersonService
{
    public Task<bool> Create(ContactPersonRegistrationForm form)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteContactPerson(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ContactPerson>> GetAllContactPersons()
    {
        throw new NotImplementedException();
    }

    public Task<ContactPerson?> GetContactPersonByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<ContactPerson?> GetContactPersonById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateContactPerson(int id, ContactPersonUpdateForm form)
    {
        throw new NotImplementedException();
    }
}
