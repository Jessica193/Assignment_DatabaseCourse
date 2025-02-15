using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

namespace BusinessLibrary.Interfaces;

public interface IContactPersonService
{
    Task<bool> Create(ContactPersonRegistrationForm form);
    Task<IEnumerable<ContactPerson>> GetAllContactPersons();
    Task<ContactPerson?> GetContactPersonById(int id);
    Task<bool> UpdateContactPerson(int id, ContactPersonUpdateForm form);
    Task<bool> DeleteContactPerson(int id);


    //NYTT
    Task<IEnumerable<ContactPerson>> GetAllContactPersonsWithCustomersAsync();
}






