using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace BusinessLibrary.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;

    public async Task<bool> CreateAsync(StatusTypeRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.Status)) return false;

        var result = await _statusTypeRepository.ExistsAsync(x => x.Status.ToLower() == form.Status.ToLower());
        if (result)
        {
            return false;
        }
        try
        {
            await _statusTypeRepository.CreateAsync(StatusTypeFactory.Create(form));
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating statustype entity :: {ex.Message}");
            return false;
        }
    }


    public async Task<IEnumerable<StatusType>> GetAllStatusTypesAsync()
    {
        var entities = await _statusTypeRepository.GetAllAsync();
        var statusTypes = entities.Select(StatusTypeFactory.Create).ToList();
        return statusTypes;
    }

    public async Task<StatusType?> GetStatusTypeByIdAsync(int id)
    {
        var result = await _statusTypeRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _statusTypeRepository.GetOneAsync(x => x.Id == id);
            var statusType = StatusTypeFactory.Create(entity);
            return statusType;
        }
        return null;
    }

    public async Task<bool> UpdateStatusTypeAsync(int id, StatusTypeUpdateForm form)
    {
        var entity = await _statusTypeRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        var updatedEntity = StatusTypeFactory.CreateUpdatedEntity(form, entity);

        try
        {
            await _statusTypeRepository.UpdateAsync(updatedEntity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating statustype entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteStatusTypeAsync(int id)
    {
        var entity = await _statusTypeRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;
        try
        {
            await _statusTypeRepository.DeleteAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting statustype entity :: {ex.Message}");
            return false;
        }
    }
}
