using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

namespace BusinessLibrary.Factories;

public static class RoleFactory
{
    public static RoleRegistrationForm Create()
    {
        return new RoleRegistrationForm();
    }

    public static RoleEntity Create(RoleRegistrationForm form)
    {
        return new RoleEntity()
        {
            Name = form.Name,
        };
    }

    public static Role Create(RoleEntity entity)
    {
        return new Role()
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    public static RoleEntity CreateUpdatedEntity(RoleUpdateForm form, RoleEntity entity)
    {
        return new RoleEntity()
        {
            Id = entity.Id,
            Name = form.Name,
        };
    }
}
