using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface IStatusTypeService
{
    Task<bool> CreateAsync(StatusTypeRegistrationForm form);
    Task<IEnumerable<StatusType>> GetAllStatusTypesAsync();
    Task<StatusType?> GetStatusTypeByIdAsync(int id);
    Task<bool> UpdateStatusTypeAsync(int id, StatusTypeUpdateForm form);
    Task<bool> DeleteStatusTypeAsync(int id);
}






