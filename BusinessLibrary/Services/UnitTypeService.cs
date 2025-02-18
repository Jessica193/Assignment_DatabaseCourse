using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace BusinessLibrary.Services;

public class UnitTypeService(IUnitTypeRepository unitTypeRepository) : IUnitTypeService
{
    private readonly IUnitTypeRepository _unitTypeRepository = unitTypeRepository;

    public async Task<bool> CreateAsync(UnitTypeRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.Unit)) return false;

        var result = await _unitTypeRepository.ExistsAsync(x => x.Unit.ToLower() == form.Unit.ToLower());
        if (result)
        {
            return false;
        }
        try
        {
            await _unitTypeRepository.CreateAsync(UnitTypeFactory.Create(form));
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating unittype entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<UnitType>> GetAllUnitTypesAsync()
    {
        var entities = await _unitTypeRepository.GetAllAsync();
        var UnitTypes = entities.Select(UnitTypeFactory.Create).ToList();
        return UnitTypes;
    }


    public async Task<UnitType?> GetUnitTypeByIdAsync(int id)
    {
        var result = await _unitTypeRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _unitTypeRepository.GetOneAsync(x => x.Id == id);
            var unitType = UnitTypeFactory.Create(entity);
            return unitType;
        }
        return null;
    }

    public async Task<bool> UpdateUnitTypeAsync(int id, UnitTypeUpdateForm form)
    {
        var entity = await _unitTypeRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        UnitTypeFactory.CreateUpdatedEntity(form, entity);

        try
        {
            await _unitTypeRepository.UpdateAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating unittype entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteUnitTypeAsync(int id)
    {
        var entity = await _unitTypeRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;
        try
        {
            await _unitTypeRepository.DeleteAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting unittype entity :: {ex.Message}");
            return false;
        }
    }
}
