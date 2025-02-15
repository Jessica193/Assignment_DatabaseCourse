using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface IRoleService
{
    Task<bool> CreateAsync(RoleRegistrationForm form);
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<Role?> GetRoleByIdAsync(int id);
    Task<bool> UpdateRoleAsync(int id, RoleUpdateForm form);
    Task<bool> DeleteRoleAsync(int id);
}






