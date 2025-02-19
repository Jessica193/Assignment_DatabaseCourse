using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace BusinessLibrary.Services;

public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<bool> CreateAsync(RoleRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.Name)) return false;

        var result = await _roleRepository.ExistsAsync(x => x.Name.ToLower() == form.Name.ToLower());
        if (result)
        {
            return false;
        }

        await _roleRepository.BeginTransactionAsync();

        try
        {
            await _roleRepository.CreateAsync(RoleFactory.Create(form));
            await _roleRepository.SaveToDatabaseAsync();
            await _roleRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _roleRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating role entity :: {ex.Message}");
            return false;
        }
    }


    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        var entities = await _roleRepository.GetAllAsync();
        var roles = entities.Select(RoleFactory.Create).ToList();
        return roles;
    }

    public async Task<Role?> GetRoleByIdAsync(int id)
    {
        var result = await _roleRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _roleRepository.GetOneAsync(x => x.Id == id);
            var role = RoleFactory.Create(entity);
            return role;
        }
        return null;
    }

    public async Task<bool> UpdateRoleAsync(int id, RoleUpdateForm form)
    {
        var entity = await _roleRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        RoleFactory.CreateUpdatedEntity(form, entity);

        await _roleRepository.BeginTransactionAsync();

        try
        {
            _roleRepository.Update(entity);
            await _roleRepository.SaveToDatabaseAsync();
            await _roleRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _roleRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating role entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteRoleAsync(int id)
    {
        var entity = await _roleRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        await _roleRepository.BeginTransactionAsync();

        try
        {
            _roleRepository.Delete(entity);
            await _roleRepository.SaveToDatabaseAsync();
            await _roleRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _roleRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting role entity :: {ex.Message}");
            return false;
        }
    }
}
