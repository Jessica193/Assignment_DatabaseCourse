﻿using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace BusinessLibrary.Services;

public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<bool> Create(RoleRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.Name)) return false;

        var result = await _roleRepository.ExistsAsync(x => x.Name.ToLower() == form.Name.ToLower());
        if (result)
        {
            return false;
        }
        try
        {
            await _roleRepository.CreateAsync(RoleFactory.Create(form));
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating role entity :: {ex.Message}");
            return false;
        }
    }


    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        var entities = await _roleRepository.GetAllAsync();
        var roles = entities.Select(RoleFactory.Create).ToList();
        return roles;
    }

    public async Task<Role?> GetRoleById(int id)
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

    public async Task<bool> UpdateRole(int id, RoleUpdateForm form)
    {
        var entity = await _roleRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        var updatedEntity = RoleFactory.CreateUpdatedEntity(form, entity);

        try
        {
            await _roleRepository.UpdateAsync(updatedEntity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating role entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteRole(int id)
    {
        var entity = await _roleRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;
        try
        {
            await _roleRepository.DeleteAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting role entity :: {ex.Message}");
            return false;
        }
    }
}
