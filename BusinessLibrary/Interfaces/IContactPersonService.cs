using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

namespace BusinessLibrary.Interfaces;

public interface IContactPersonService
{
    Task<bool> CreateAsync(ContactPersonRegistrationForm form);
    Task<IEnumerable<ContactPerson>> GetAllContactPersonsAsync();
    Task<ContactPerson?> GetContactPersonByIdAsync(int id);
    Task<bool> UpdateContactPersonAsync(int id, ContactPersonUpdateForm form);
    Task<bool> DeleteContactPersonAsync(int id);
    Task<IEnumerable<ContactPerson>> GetAllContactPersonsWithCustomersAsync();
}






