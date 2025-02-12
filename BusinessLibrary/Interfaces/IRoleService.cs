using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface IRoleService
{
    Task<bool> Create(RoleRegistrationForm form);
    Task<IEnumerable<Role>> GetAllRoles();
    Task<Role?> GetRoleById(int id);
    Task<bool> UpdateRole(int id, RoleUpdateForm form);
    Task<bool> DeleteRole(int id);
}






