using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

namespace BusinessLibrary.Factories;

public static class UnitTypeFactory
{
    public static UnitTypeRegistrationForm Create()
    {
        return new UnitTypeRegistrationForm();
    }

    public static UnitTypeEntity Create(UnitTypeRegistrationForm form)
    {
        return new UnitTypeEntity()
        {
            Unit = form.Unit,
        };
    }

    public static UnitType Create(UnitTypeEntity entity)
    {
        return new UnitType()
        {
            Id = entity.Id,
            Unit = entity.Unit,
        };
    }




}
