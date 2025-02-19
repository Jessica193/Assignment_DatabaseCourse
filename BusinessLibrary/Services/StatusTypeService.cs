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

        await _statusTypeRepository.BeginTransactionAsync();

        try
        {
            await _statusTypeRepository.CreateAsync(StatusTypeFactory.Create(form));
            await _statusTypeRepository.SaveToDatabaseAsync();
            await _statusTypeRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _statusTypeRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating status type entity :: {ex.Message}");
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

        StatusTypeFactory.CreateUpdatedEntity(form, entity);

        await _statusTypeRepository.BeginTransactionAsync();

        try
        {
            _statusTypeRepository.Update(entity);
            await _statusTypeRepository.SaveToDatabaseAsync();
            await _statusTypeRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _statusTypeRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating status type entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteStatusTypeAsync(int id)
    {
        var entity = await _statusTypeRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        await _statusTypeRepository.BeginTransactionAsync();

        try
        {
            _statusTypeRepository.Delete(entity);
            await _statusTypeRepository.SaveToDatabaseAsync();
            await _statusTypeRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _statusTypeRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting status type entity :: {ex.Message}");
            return false;
        }
    }
}
