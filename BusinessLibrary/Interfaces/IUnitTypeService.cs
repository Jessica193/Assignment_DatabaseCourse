using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface IUnitTypeService
{
    Task<bool> CreateAsync(UnitTypeRegistrationForm form);
    Task<IEnumerable<UnitType>> GetAllUnitTypesAsync();
    Task<UnitType?> GetUnitTypeByIdAsync(int id);
    Task<bool> UpdateUnitTypeAsync(int id, UnitTypeUpdateForm form);
    Task<bool> DeleteUnitTypeAsync(int id);
}






