using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface IUnitTypeService
{
    Task<bool> Create(UnitTypeRegistrationForm form);
    Task<IEnumerable<UnitType>> GetAllUnitTypes();
    Task<UnitType?> GetUnitTypeById(int id);
    Task<bool> UpdateUnitType(int id, UnitTypeUpdateForm form);
    Task<bool> DeleteUnitType(int id);
}






