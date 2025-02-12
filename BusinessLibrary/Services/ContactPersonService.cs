using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace BusinessLibrary.Services;

public class ContactPersonService(IContactPersonRepository contactPersonRepository) : IContactPersonService
{
    private readonly IContactPersonRepository _contactPersonRepository = contactPersonRepository;

    public async Task<bool> Create(ContactPersonRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.FirstName) || string.IsNullOrWhiteSpace(form.LastName) || string.IsNullOrWhiteSpace(form.Email) || string.IsNullOrWhiteSpace(form.PhoneNumber)) return false;

        var result = await _contactPersonRepository.ExistsAsync(x => x.Email.ToLower() == form.Email.ToLower());
        if (result)
        {
            return false;
        }
        try
        {
            await _contactPersonRepository.CreateAsync(ContactPersonFactory.Create(form));
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating contact person entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<ContactPerson>> GetAllContactPersons()
    {
        var entities = await _contactPersonRepository.GetAllAsync();
        var contactPersons = entities.Select(ContactPersonFactory.Create).ToList();
        return contactPersons;
    }

    public async Task<ContactPerson?> GetContactPersonById(int id)
    {
        var result = await _contactPersonRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _contactPersonRepository.GetOneAsync(x => x.Id == id);
            var contactPerson = ContactPersonFactory.Create(entity);
            return contactPerson;
        }
        return null;
    }

    public async Task<bool> UpdateContactPerson(int id, ContactPersonUpdateForm form)
    {
        var entity = await _contactPersonRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        var updatedEntity = ContactPersonFactory.CreateUpdatedEntity(form, entity);

        try
        {
            await _contactPersonRepository.UpdateAsync(updatedEntity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating contact person entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteContactPerson(int id)
    {
        var entity = await _contactPersonRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;
        try
        {
            await _contactPersonRepository.DeleteAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting contact person entity :: {ex.Message}");
            return false;
        }
    }

   
}
