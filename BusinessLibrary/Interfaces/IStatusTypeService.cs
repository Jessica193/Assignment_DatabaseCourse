using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface IStatusTypeService
{
    Task<bool> Create(StatusTypeRegistrationForm form);
    Task<IEnumerable<StatusType>> GetAllStatusTypes();
    Task<StatusType?> GetStatusTypeById(int id);
    Task<bool> UpdateStatusType(int id, StatusTypeUpdateForm form);
    Task<bool> DeleteStatusType(int id);
}






