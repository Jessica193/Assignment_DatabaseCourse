using BusinessLibrary.Dtos;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;

namespace BusinessLibrary.Services;

public class RoleService : IRoleService
{
    public Task<bool> Create(RoleRegistrationForm form)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteRole(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Role>> GetAllRoles()
    {
        throw new NotImplementedException();
    }

    public Task<Role?> GetRoleById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateRole(int id, RoleUpdateForm form)
    {
        throw new NotImplementedException();
    }
}
